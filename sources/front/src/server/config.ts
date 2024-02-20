import {Configuration} from "@azure/msal-browser";

export const fetchConfig = {
    getScope() {
        return "";
    },
    getMsalConfig() {
        return {} as Configuration;
    }
};
