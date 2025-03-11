import {advancedSearchCss, summaryCss, tableFor} from "@focus4/collections";
import {useLoad} from "@focus4/forms";
import {stringFor} from "@focus4/stores";

import {ProfilItemEntity} from "../../model/securite/profil/profil-item";
import {getProfils} from "../../services/securite/profil";
import {profilStore} from "../../stores/profil";

import {router} from "../../router";

import css from "./__style__/table.css";

export function ProfilTable() {
    const [isLoading] = useLoad(profilStore.profils, a => a.params().load(getProfils));

    return (
        <>
            <div className={advancedSearchCss.topRow}>
                <div className={summaryCss.summary}>
                    <span>
                        <strong>{profilStore.profils.length}</strong> profils
                    </span>
                </div>
            </div>
            {tableFor({
                data: profilStore.profils,
                itemKey: pro => pro.id.value,
                onLineClick: pro => router.to(x => x("profils")(pro.id.value!)),
                isLoading,
                columns: [
                    {
                        title: ProfilItemEntity.libelle.label,
                        content: pro => stringFor(pro.libelle)
                    },
                    {
                        title: ProfilItemEntity.nombreUtilisateurs.label,
                        content: pro => <div className={css.users}>{stringFor(pro.nombreUtilisateurs)}</div>
                    }
                ]
            })}
            <br />
        </>
    );
}
