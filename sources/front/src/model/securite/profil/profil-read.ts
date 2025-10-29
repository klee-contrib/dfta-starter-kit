////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

import {e, entity, EntityToType} from "@focus4/entities";
import {DO_CODE_LISTE, DO_DATE_HEURE, DO_ID, DO_LIBELLE} from "../../../domains";

import {DroitCode} from "../references";
import {UtilisateurItemEntity} from "../utilisateur/utilisateur-item";

export type ProfilRead = EntityToType<ProfilReadEntityType>;
export type ProfilReadEntityType = typeof ProfilReadEntity;

export const ProfilReadEntity = entity({
    id: e.field(DO_ID, f => f.optional()
        .label("securite.profil.profil.id")
    ),
    libelle: e.field(DO_LIBELLE, f => f
        .label("securite.profil.profil.libelle")
    ),
    dateCreation: e.field(DO_DATE_HEURE, f => f
        .label("common.entityListeners.dateCreation")
    ),
    dateModification: e.field(DO_DATE_HEURE, f => f.optional()
        .label("common.entityListeners.dateModification")
    ),
    droitCodes: e.field(DO_CODE_LISTE, f => f.type<DroitCode[]>()
        .label("securite.droit.code")
    ),
    utilisateurs: e.list(UtilisateurItemEntity, f => f
        .label("securite.profil.profilRead.utilisateurs")
    )
});
