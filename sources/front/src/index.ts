import "focus4";
import {initColorScheme} from "@focus4/styling";

import {router} from "./router";
import {config, ensureSignedIn} from "./server/server";
import {getProfils} from "./services/securite/profil";

initColorScheme();

const res = await fetch("./config.json");
Object.assign(config, await res.json());
await ensureSignedIn();

await getProfils();

await router.start();

await import("./locale");
await import("./views");
