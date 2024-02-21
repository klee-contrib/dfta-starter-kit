import {coreFetch} from "@focus4/core";

import {getTokenRedirect} from "./msal";

export {config, ensureSignedIn, signOut, userStore} from "./msal";

export async function fetch(
    method: "DELETE" | "GET" | "POST" | "PUT",
    url: string,
    {body, query}: {body?: {}; query?: {}} = {},
    options: RequestInit = {}
) {
    const auth = await getTokenRedirect();
    if (auth) {
        options.headers = {
            Authorization: `Bearer ${auth.accessToken}`
        };
        options.credentials = "omit";

        return coreFetch(method, url, {body, query}, options);
    }
}
