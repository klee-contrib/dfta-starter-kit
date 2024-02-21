import "focus4";
import {initColorScheme} from "@focus4/styling";

import {getProfils} from "./services/securite/profil";

import {router} from "./router";
import {ensureSignedIn} from "./server";
import {config} from "./server/msal";

initColorScheme();

fetch("./config.json").then(async res => {
    Object.assign(config, await res.json());
    await ensureSignedIn();

    await getProfils();

    await router.start();

    await import("./locale");
    await import("./views");
});
