import {useState} from "react";

import {advancedSearchFor} from "@focus4/collections";
import {messageStore} from "@focus4/core";
import {useLoad} from "@focus4/forms";
import {Dialog} from "@focus4/layout";

import {UtilisateurItem} from "../../model/securite/utilisateur/utilisateur-item";
import {searchUtilisateur} from "../../services/securite/utilisateur";
import {utilisateurListStore} from "../../stores/utilisateur";

import {UtilisateurDelete} from "./delete";
import {UtilisateurLine} from "./line";

export function UtilisateurList() {
    const [utiDelete, setUtiDelete] = useState<UtilisateurItem>();
    const [manyDialogActive, setManyDialogActive] = useState(false);

    useLoad(utilisateurListStore, a => a.params().load(() => searchUtilisateur()));

    return (
        <>
            {advancedSearchFor({
                store: utilisateurListStore,
                facetBoxPosition: "none",
                canRemoveSort: false,
                hasSelection: true,
                hasSearchBar: true,
                orderableColumnList: [
                    {label: "Nom croissant", sort: [{fieldName: "nom", sortDesc: false}]},
                    {label: "Nom décroissant", sort: [{fieldName: "nom", sortDesc: true}]},
                    {label: "Prénom croissant", sort: [{fieldName: "prenom", sortDesc: false}]},
                    {label: "Prénom décroissant", sort: [{fieldName: "prenom", sortDesc: true}]}
                ],
                operationList: [{action: () => setManyDialogActive(true), label: "Supprimer", icon: "delete"}],
                listProps: {
                    itemKey: i => i.id,
                    operationList: uti => [
                        {action: () => setUtiDelete(uti), icon: "delete", type: "icon-tooltip", label: "Supprimer"}
                    ],
                    LineComponent: UtilisateurLine,
                    perPage: 10
                }
            })}
            <UtilisateurDelete
                closeDialog={() => setUtiDelete(undefined)}
                onDelete={utilisateurListStore.search}
                utilisateur={utiDelete}
            />
            <Dialog
                actions={[
                    {
                        label: "Confirmer",
                        color: "primary",
                        variant: "elevated-filled",
                        onClick: async () => {
                            messageStore.addWarningMessage("Méthode non implémentée...");
                            setManyDialogActive(false);
                            utilisateurListStore.search();
                        }
                    },
                    {label: "Annuler", onClick: () => setManyDialogActive(false)}
                ]}
                active={manyDialogActive}
                onOverlayClick={() => setManyDialogActive(false)}
                title="Suppression de plusieurs utilisateurs"
            >
                Êtes vous sûr de vouloir supprimer ces {utilisateurListStore.selectedItems.size} utilisateurs ?
                <br />
                <br />
                (La méthode n'est pas implémentée)
            </Dialog>
        </>
    );
}
