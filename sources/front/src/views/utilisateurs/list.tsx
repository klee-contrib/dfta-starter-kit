import {useEffect, useState} from "react";
import {useTranslation} from "react-i18next";

import {ActionBar, listFor, Summary} from "@focus4/collections";
import {messageStore} from "@focus4/core";
import {fieldFor, selectFor} from "@focus4/forms";
import {Content, Dialog, Panel} from "@focus4/layout";

import {TypeUtilisateurCode} from "../../model/securite/references";
import {UtilisateurItem} from "../../model/securite/utilisateur/utilisateur-item";
import {referenceStore} from "../../stores/references";
import {utilisateurSearchStore} from "../../stores/utilisateur";

import {UtilisateurDelete} from "./delete";
import {UtilisateurLine} from "./line";

export function UtilisateurList() {
    const {t} = useTranslation();

    const [utiDelete, setUtiDelete] = useState<UtilisateurItem>();
    const [manyDialogActive, setManyDialogActive] = useState(false);

    const orderableColumnList = [
        {label: t("app.user.ordering.nameAsc"), sort: [{fieldName: "Nom", sortDesc: false}]},
        {label: t("app.user.ordering.nameDesc"), sort: [{fieldName: "Nom", sortDesc: true}]},
        {label: t("app.user.ordering.surnameAsc"), sort: [{fieldName: "Prenom", sortDesc: false}]},
        {label: t("app.user.ordering.surnameDesc"), sort: [{fieldName: "Prenom", sortDesc: true}]}
    ];

    useEffect(() => {
        utilisateurSearchStore.search();
    }, []);

    return (
        <Content>
            <Panel collapsible initiallyCollapsed title="Critères de recherche">
                {fieldFor(utilisateurSearchStore.criteria.nom)}
                {fieldFor(utilisateurSearchStore.criteria.prenom)}
                {selectFor(utilisateurSearchStore.criteria.typeUtilisateurCode, referenceStore.typeUtilisateur)}
            </Panel>
            <Summary
                canRemoveSort={false}
                chipKeyResolver={async (_, code, value) => {
                    if (code.includes("typeUtilisateurCode")) {
                        return referenceStore.typeUtilisateur.getLabel(value as TypeUtilisateurCode);
                    }
                }}
                orderableColumnList={orderableColumnList}
                store={utilisateurSearchStore}
            />
            <ActionBar
                hasSelection
                operationList={[
                    {action: () => setManyDialogActive(true), label: t("app.user.delete.action"), icon: "delete"}
                ]}
                orderableColumnList={orderableColumnList}
                store={utilisateurSearchStore}
            />
            {listFor({
                store: utilisateurSearchStore,
                hasSelection: true,
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
            })}
            <UtilisateurDelete
                closeDialog={() => setUtiDelete(undefined)}
                onDelete={utilisateurSearchStore.search}
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
                            utilisateurSearchStore.search();
                        }
                    },
                    {label: t("app.user.delete.cancel"), onClick: () => setManyDialogActive(false)}
                ]}
                active={manyDialogActive}
                onOverlayClick={() => setManyDialogActive(false)}
                title={t("app.user.delete.titleMany")}
            >
                {t("app.user.delete.textMany", {param: utilisateurSearchStore.selectedItems.size})}
                <br />
                <br />
                (La méthode n'est pas implémentée)
            </Dialog>
        </Content>
    );
}
