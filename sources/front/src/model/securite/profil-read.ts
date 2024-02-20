////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

import {EntityToType, FieldEntry2, ListEntry} from "@focus4/stores";
import {DO_CODE_LISTE, DO_DATE_HEURE, DO_ID, DO_LIBELLE} from "../../domains";

import {DroitCode} from "./references";
import {UtilisateurItemEntity, UtilisateurItemEntityType} from "./utilisateur-item";

export type ProfilRead = EntityToType<ProfilReadEntityType>
export interface ProfilReadEntityType {
    id: FieldEntry2<typeof DO_ID, number>,
    libelle: FieldEntry2<typeof DO_LIBELLE, string>,
    dateCreation: FieldEntry2<typeof DO_DATE_HEURE, string>,
    dateModification: FieldEntry2<typeof DO_DATE_HEURE, string>,
    droitCodes: FieldEntry2<typeof DO_CODE_LISTE, DroitCode[]>,
    utilisateurs: ListEntry<UtilisateurItemEntityType>
}

export const ProfilReadEntity: ProfilReadEntityType = {
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
    dateCreation: {
        type: "field",
        name: "dateCreation",
        domain: DO_DATE_HEURE,
        isRequired: true,
        label: "common.entityListeners.dateCreation"
    },
    dateModification: {
        type: "field",
        name: "dateModification",
        domain: DO_DATE_HEURE,
        isRequired: false,
        label: "common.entityListeners.dateModification"
    },
    droitCodes: {
        type: "field",
        name: "droitCodes",
        domain: DO_CODE_LISTE,
        isRequired: true,
        label: "securite.droit.code"
    },
    utilisateurs: {
        type: "list",
        entity: UtilisateurItemEntity
    }
}
