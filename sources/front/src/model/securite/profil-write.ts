////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

import {EntityToType, FieldEntry2} from "@focus4/stores";
import {DO_CODE_LISTE, DO_LIBELLE} from "../../domains";

import {DroitCode} from "./references";

export type ProfilWrite = EntityToType<ProfilWriteEntityType>
export interface ProfilWriteEntityType {
    libelle: FieldEntry2<typeof DO_LIBELLE, string>,
    droitCodes: FieldEntry2<typeof DO_CODE_LISTE, DroitCode[]>
}

export const ProfilWriteEntity: ProfilWriteEntityType = {
    libelle: {
        type: "field",
        name: "libelle",
        domain: DO_LIBELLE,
        isRequired: true,
        label: "securite.profil.libelle"
    },
    droitCodes: {
        type: "field",
        name: "droitCodes",
        domain: DO_CODE_LISTE,
        isRequired: true,
        label: "securite.droit.code"
    }
}
