import {
    AccountInfo,
    BrowserAuthError,
    InteractionRequiredAuthError,
    LogLevel,
    PublicClientApplication
} from "@azure/msal-browser";

import {UserStore as FocusUserStore} from "@focus4/core";

export const config = {
    version: "",
    clientId: "",
    tenantId: "",
    audience: "",
    environment: ""
};

let msal: PublicClientApplication;

export async function ensureSignedIn() {
    if (!msal) {
        msal = new PublicClientApplication({
            auth: {
                clientId: config.clientId,
                authority: `https://login.microsoftonline.com/${config.tenantId}`,
                redirectUri: window.location.origin + window.location.pathname
            },
            cache: {
                cacheLocation: "localStorage"
            },
            system: {
                loggerOptions: {
                    loggerCallback: (level, message, containsPii) => {
                        if (containsPii) {
                            return;
                        }
                        switch (level) {
                            case LogLevel.Error:
                                console.error(message);
                                return;
                            case LogLevel.Warning:
                                console.warn(message);
                                return;
                            default:
                                return;
                        }
                    }
                }
            }
        });
    }
    await msal.initialize();
    await msal.handleRedirectPromise();
}

export async function getTokenRedirect() {
    const request = {
        scopes: ["openid", "profile", `${config.audience}/access`],
        account: userStore.account
    };
    try {
        return await msal.acquireTokenSilent(request);
    } catch (error: unknown) {
        console.warn("silent token acquisition fails. acquiring token using redirect");
        if (error instanceof InteractionRequiredAuthError || error instanceof BrowserAuthError) {
            // Fallback to interaction when silent call fails
            return msal.acquireTokenRedirect(request);
        } else {
            console.warn(error);
        }
    }
}

export function signOut() {
    return msal.logoutRedirect({account: userStore.account});
}

class UserStore extends FocusUserStore {
    get account(): AccountInfo | undefined {
        return msal.getAllAccounts()[0];
    }

    get login() {
        return this.account?.name ?? "";
    }
}

export const userStore = new UserStore();
