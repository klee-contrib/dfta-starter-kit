import {useState} from "react";
import {useTranslation} from "react-i18next";

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
    const {t} = useTranslation();

    const [utiDelete, setUtiDelete] = useState<UtilisateurItem>();
    const [manyDialogActive, setManyDialogActive] = useState(false);

    useLoad(utilisateurListStore, a => a.params().load(() => searchUtilisateur()));

    return (
        <>
            {advancedSearchFor({
                store: utilisateurListStore,
                facetBoxPosition: "none",
                hasSearchBar: true,
                hasSelection: true,
                orderableColumnList: [
                    {label: t("app.user.ordering.nameAsc"), sort: [{fieldName: "nom", sortDesc: false}]},
                    {label: t("app.user.ordering.nameDesc"), sort: [{fieldName: "nom", sortDesc: true}]},
                    {label: t("app.user.ordering.surnameAsc"), sort: [{fieldName: "prenom", sortDesc: false}]},
                    {label: t("app.user.ordering.surnameDesc"), sort: [{fieldName: "prenom", sortDesc: true}]}
                ],
                operationList: [
                    {action: () => setManyDialogActive(true), label: t("app.user.delete.action"), icon: "delete"}
                ],
                listProps: {
                    itemKey: i => i.id,
                    operationList: uti => [
                        {
                            action: () => setUtiDelete(uti),
                            icon: "delete",
                            type: "icon-tooltip",
                            label: t("app.user.delete.action")
                        }
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
                        label: t("app.user.delete.confirm"),
                        color: "primary",
                        variant: "elevated-filled",
                        onClick: async () => {
                            messageStore.addWarningMessage("Méthode non implémentée...");
                            setManyDialogActive(false);
                            utilisateurListStore.search();
                        }
                    },
                    {label: t("app.user.delete.cancel"), onClick: () => setManyDialogActive(false)}
                ]}
                active={manyDialogActive}
                onOverlayClick={() => setManyDialogActive(false)}
                title={t("app.user.delete.titleMany")}
            >
                {t("app.user.delete.textMany", {param: utilisateurListStore.selectedItems.size})}
                <br />
                <br />
                (La méthode n'est pas implémentée)
            </Dialog>
        </>
    );
}
