import {ZodString} from "zod";

import {SelectRadio} from "@focus4/form-toolbox";
import {makeServerCollectionStore, makeStoreNode} from "@focus4/stores";

import {UtilisateurCriteriaEntity} from "../model/securite/utilisateur/utilisateur-criteria";
import {UtilisateurReadEntity} from "../model/securite/utilisateur/utilisateur-read";
import {searchUtilisateur} from "../services/securite/utilisateur";

export const utilisateurSearchStore = makeServerCollectionStore(searchUtilisateur, UtilisateurCriteriaEntity, {
    sort: [{fieldName: "Prenom"}],
    top: 20,
    criteriaMode: "debounced",
    criteriaBuilder: c =>
        c.patch("typeUtilisateurCode", f =>
            f.metadata({
                SelectComponent: SelectRadio<ZodString>,
                selectProps: {hasUndefined: "first-option", undefinedLabel: "Tous"}
            })
        )
});

export const utilisateurStore = makeStoreNode({
    utilisateur: UtilisateurReadEntity
});
