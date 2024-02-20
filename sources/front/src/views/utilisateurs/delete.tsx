import {messageStore} from "@focus4/core";
import {Dialog} from "@focus4/layout";

import {UtilisateurItem} from "../../model/securite/utilisateur-item";
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
    return (
        <Dialog
            actions={[
                {
                    label: "Confirmer",
                    color: "primary",
                    variant: "filled",
                    onClick: async () => {
                        await deleteUtilisateur(utilisateur!.id!);
                        messageStore.addSuccessMessage("L'utilisateur a bien été supprimé");
                        closeDialog();
                        onDelete();
                    }
                },
                {label: "Annuler", onClick: closeDialog}
            ]}
            active={!!utilisateur}
            title="Suppression d'un utilisateur"
        >
            Êtes-vous sûr de vouloir supprimer l'utilisateur {utilisateur?.prenom} {utilisateur?.nom} ?
        </Dialog>
    );
}
