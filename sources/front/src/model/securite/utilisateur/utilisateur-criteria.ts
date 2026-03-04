////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

import {e, entity, EntityToType} from "@focus4/entities";
import {DO_BOOLEEN, DO_CODE, DO_CODE_LISTE, DO_DATE, DO_EMAIL, DO_ID, DO_LIBELLE} from "../../../domains";

import {TypeUtilisateurCode} from "../references";

export type UtilisateurCriteria = EntityToType<UtilisateurCriteriaEntityType>;
export type UtilisateurCriteriaEntityType = typeof UtilisateurCriteriaEntity;

export const UtilisateurCriteriaEntity = entity({
    nom: e.field(DO_LIBELLE, f => f.optional()
        .label("securite.utilisateur.utilisateur.nom")
    ),
    prenom: e.field(DO_LIBELLE, f => f.optional()
        .label("securite.utilisateur.utilisateur.prenom")
    ),
    email: e.field(DO_EMAIL, f => f.optional()
        .label("securite.utilisateur.utilisateur.email")
    ),
    dateNaissance: e.field(DO_DATE, f => f.optional()
        .label("securite.utilisateur.utilisateur.dateNaissance")
    ),
    actif: e.field(DO_BOOLEEN, f => f.optional()
        .label("securite.utilisateur.utilisateur.actif")
    ),
    profilId: e.field(DO_ID, f => f.optional()
        .label("securite.utilisateur.utilisateur.profilId")
    ),
    typeUtilisateurCode: e.field(DO_CODE, f => f.type<TypeUtilisateurCode>().optional()
        .label("securite.utilisateur.utilisateur.typeUtilisateurCode")
    ),
    query: e.field(DO_LIBELLE, f => f.optional()
        .label("common.criteria.query")
    ),
    searchFields: e.field(DO_CODE_LISTE, f => f.optional()
        .label("common.criteria.searchFields")
    ),
    sourceFields: e.field(DO_CODE_LISTE, f => f.optional()
        .label("common.criteria.sourceFields")
    )
});
