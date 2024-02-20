import {makeEntityStore} from "@focus4/stores";

import {ProfilItemEntity} from "../model/securite/profil-item";
import {ProfilReadEntity} from "../model/securite/profil-read";

export const profilStore = makeEntityStore({
    profil: ProfilReadEntity,
    profils: [ProfilItemEntity]
});
