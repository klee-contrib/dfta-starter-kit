import {useTranslation} from "react-i18next";

import {messageStore} from "@focus4/core";
import {Dialog} from "@focus4/layout";

import {UtilisateurItem} from "../../model/securite/utilisateur/utilisateur-item";
import {deleteUtilisateur} from "../../services/securite/utilisateur";

export function UtilisateurDelete({
    closeDialog,
    onDelete,
    utilisateur
}: {
    closeDialog: () => void;
    onDelete: () => void;
    utilisateur?: UtilisateurItem;
}) {
    const {t} = useTranslation();
    return (
        <Dialog
            actions={[
                {
                    label: t("app.user.delete.confirm"),
                    color: "primary",
                    variant: "filled",
                    onClick: async () => {
                        await deleteUtilisateur(utilisateur!.id!);
                        messageStore.addSuccessMessage("app.user.delete.success");
                        closeDialog();
                        onDelete();
                    }
                },
                {label: t("app.user.delete.cancel"), onClick: closeDialog}
            ]}
            active={!!utilisateur}
            title={t("app.user.delete.title")}
        >
            {t("app.user.delete.text", {param: `${utilisateur?.prenom ?? ""} ${utilisateur?.nom ?? ""}`})}
        </Dialog>
    );
}
