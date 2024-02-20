import {
    AccountInfo,
    BrowserAuthError,
    InteractionRequiredAuthError,
    PublicClientApplication
} from "@azure/msal-browser";

import {UserStore as FocusUserStore} from "@focus4/core";

import {fetchConfig} from "./config";

let msal: PublicClientApplication | undefined;

export function getMsal() {
    if (!msal) {
        msal = new PublicClientApplication(fetchConfig.getMsalConfig());
    }

    return msal;
}

export async function getTokenRedirect() {
    const request = {
        scopes: ["openid", "profile", fetchConfig.getScope()],
        account: userStore.account
    };
    try {
        return await getMsal().acquireTokenSilent(request);
    } catch (error: unknown) {
        console.warn("silent token acquisition fails. acquiring token using redirect");
        if (error instanceof InteractionRequiredAuthError || error instanceof BrowserAuthError) {
            // Fallback to interaction when silent call fails
            return getMsal().acquireTokenRedirect(request);
        } else {
            console.warn(error);
        }
    }
}

class UserStore extends FocusUserStore {
    get account(): AccountInfo | undefined {
        return getMsal().getAllAccounts()[0];
    }

    get login() {
        return this.account?.name ?? "";
    }
}

export const userStore = new UserStore();
