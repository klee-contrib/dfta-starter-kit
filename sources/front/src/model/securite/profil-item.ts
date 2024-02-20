////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

import {EntityToType, FieldEntry2} from "@focus4/stores";
import {DO_ENTIER, DO_ID, DO_LIBELLE} from "../../domains";

export type ProfilItem = EntityToType<ProfilItemEntityType>
export interface ProfilItemEntityType {
    id: FieldEntry2<typeof DO_ID, number>,
    libelle: FieldEntry2<typeof DO_LIBELLE, string>,
    nombreUtilisateurs: FieldEntry2<typeof DO_ENTIER, number>
}

export const ProfilItemEntity: ProfilItemEntityType = {
    id: {
        type: "field",
        name: "id",
        domain: DO_ID,
        isRequired: false,
        label: "securite.profil.id"
    },
    libelle: {
        type: "field",
        name: "libelle",
        domain: DO_LIBELLE,
        isRequired: true,
        label: "securite.profil.libelle"
    },
    nombreUtilisateurs: {
        type: "field",
        name: "nombreUtilisateurs",
        domain: DO_ENTIER,
        isRequired: true,
        label: "securite.profilItem.nombreUtilisateurs"
    }
}
