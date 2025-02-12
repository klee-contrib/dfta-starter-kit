import {useObserver} from "mobx-react";

import {SelectChips} from "@focus4/form-toolbox";
import {fieldFor, Form, selectFor, useFormActions, useFormNode} from "@focus4/forms";
import {Panel} from "@focus4/layout";
import {Slider} from "@focus4/toolbox";

import {getProfil, updateProfil} from "../../../services/securite/profil";
import {profilStore} from "../../../stores/profil";
import {referenceStore} from "../../../stores/references";

import {DO_ENTIER} from "../../../domains";
import {router} from "../../../router";

export function ProfilInfos() {
    const entity = useFormNode(profilStore.profil, e =>
        e
            .remove("id", "dateCreation", "dateModification", "utilisateurs")
            .add("nombreUtilisateursMax", f =>
                f
                    .domain(DO_ENTIER)
                    .metadata({
                        label: "Nombre maximal d'utilisateurs",
                        comment:
                            "Ce champ n'est qu'une excuse pour poser un slider, mais si sa valeur dépasse 12 alors le composant de sélection de droits va changer ;)",
                        InputComponent: Slider,
                        inputProps: {labeled: true, ticks: true, max: 20},
                        labelProps: {
                            showTooltip: true
                        }
                    })
                    .value(8)
            )
            .patch("droitCodes", (f, node) =>
                f.metadata(() =>
                    node.nombreUtilisateursMax.value! > 12
                        ? {SelectComponent: SelectChips<"string-array">, selectProps: {hasSelectAll: true}}
                        : {}
                )
            )
    );

    const actions = useFormActions(entity, a =>
        a
            .params(() => router.state.profils.proId)
            .load(getProfil)
            .update(updateProfil)
            .withConfirmation(router)
    );

    return useObserver(() => (
        <Form {...actions.formProps}>
            <Panel title="Informations" {...actions.panelProps}>
                {fieldFor(entity.libelle)}
                {selectFor(entity.droitCodes, referenceStore.droit)}
                {fieldFor(entity.nombreUtilisateursMax)}
                {!entity.form.isEdit ? fieldFor(entity.sourceNode.dateCreation) : null}
                {!entity.form.isEdit && entity.sourceNode.dateModification.value
                    ? fieldFor(entity.sourceNode.dateModification)
                    : null}
            </Panel>
        </Form>
    ));
}
