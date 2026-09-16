import {observer} from "mobx-react";
import {useTranslation} from "react-i18next";

import {listFor} from "@focus4/collections";
import {Panel} from "@focus4/layout";

import {profilStore} from "../../../stores/profil";

import {UtilisateurLine} from "../../utilisateurs/line";

export const ProfilUtilisateurs = observer(function ProfilUtilisateurs() {
    const {t} = useTranslation();

    return (
        <Panel icon="group" title={t("app.profile.users", {param: profilStore.profil.utilisateurs.length})}>
            {listFor({
                data: profilStore.profil.utilisateurs.getValues(),
                itemKey: uti => uti.id,
                LineComponent: props => <UtilisateurLine {...props} profil />
            })}
        </Panel>
    );
});
