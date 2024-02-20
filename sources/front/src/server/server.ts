import {LogLevel} from "@azure/msal-browser";

import {fetchConfig} from "./config";
import {getMsal, userStore} from "./msal";

export const config = {
    version: "",
    clientId: "",
    tenantId: "",
    audience: ""
};

export async function ensureSignedIn() {
    const msal = getMsal();
    await msal.initialize();
    await msal.handleRedirectPromise();
}

export function signOut() {
    return getMsal().logoutRedirect({account: userStore.account});
}

fetchConfig.getScope = () => `api://${config.audience}/access`;
fetchConfig.getMsalConfig = () => ({
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
                }
            }
        }
    }
});
