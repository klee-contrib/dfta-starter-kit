import {useObserver} from "mobx-react";

import {toBem} from "@focus4/styling";
import {FontIcon, Ripple} from "@focus4/toolbox";

import {UtilisateurItem} from "../../model/securite/utilisateur-item";
import {referenceStore} from "../../stores/references";

import {router} from "../../router";

import css from "./__style__/line.css";
const theme = toBem(css);

export function UtilisateurLine({data, profil}: {data: UtilisateurItem; profil?: boolean}) {
    return useObserver(() => (
        <Ripple>
            <a className={theme.line({profil})} href={router.href(x => x("utilisateurs")(data.id!))}>
                <FontIcon className={theme.icon()}>person</FontIcon>
                <div>
                    <div className={theme.text({title: true})}>
                        {data.prenom} {data.nom}
                    </div>
                    <div className={theme.text()}>{data.email}</div>
                    <div className={theme.text({sub: true})}>
                        {referenceStore.typeUtilisateur.getLabel(data.typeUtilisateurCode)}
                    </div>
                </div>
            </a>
        </Ripple>
    ));
}
