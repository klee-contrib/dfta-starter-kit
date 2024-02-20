import {listFor} from "@focus4/collections";
import {Panel} from "@focus4/forms";
import {toFlatValues} from "@focus4/stores";

import {profilStore} from "../../../stores/profil";

import {UtilisateurLine} from "../../utilisateurs/line";

export function ProfilUtilisateurs() {
    return (
        <Panel title={`Utilisateurs (${profilStore.profil.utilisateurs.length})`}>
            {listFor({
                data: toFlatValues(profilStore.profil.utilisateurs),
                itemKey: uti => uti.id,
                LineComponent: props => <UtilisateurLine {...props} profil />
            })}
        </Panel>
    );
}
