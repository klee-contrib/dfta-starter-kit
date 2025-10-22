import {useObserver} from "mobx-react";
import {useTranslation} from "react-i18next";

import {listFor} from "@focus4/collections";
import {Panel} from "@focus4/layout";
import {toFlatValues} from "@focus4/stores";

import {profilStore} from "../../../stores/profil";

import {UtilisateurLine} from "../../utilisateurs/line";

export function ProfilUtilisateurs() {
    const {t} = useTranslation();

    return useObserver(() => (
        <Panel icon="group" title={t("app.profile.users", {param: profilStore.profil.utilisateurs.length})}>
            {listFor({
                data: toFlatValues(profilStore.profil.utilisateurs),
                itemKey: uti => uti.id,
                LineComponent: props => <UtilisateurLine {...props} profil />
            })}
        </Panel>
    ));
}
