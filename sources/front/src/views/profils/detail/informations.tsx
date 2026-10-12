import {observer} from "mobx-react";
import {ZodArray, ZodString} from "zod";

import {SelectChips} from "@focus4/form-toolbox";
import {fieldFor, Form, selectFor, useFormActions, useFormNode, useReferenceTracking} from "@focus4/forms";
import {Panel} from "@focus4/layout";
import {Slider} from "@focus4/toolbox";

import {ProfilWriteEntity} from "../../../model/securite/profil/profil-write";
import {getProfil, updateProfil} from "../../../services/securite/profil";
import {profilStore} from "../../../stores/profil";
import {referenceStore} from "../../../stores/references";

import {DO_ENTIER, DO_MONTANT} from "../../../domains";
import {router} from "../../../router";

export const ProfilInfos = observer(function ProfilInfos() {
    const entity = useFormNode(profilStore.profil, e =>
        e
            .patchAllTo(ProfilWriteEntity)
            .add("nombreUtilisateursMax", f =>
                f
                    .domain(DO_ENTIER)
                    .metadata({
                        label: "app.profile.maxUsers",
                        comment:
                            "Ce champ n'est qu'une excuse pour poser un slider, mais si sa valeur est inférieure à 1000 alors le composant de sélection de droits va changer ;)",
                        InputComponent: Slider,
                        inputProps: {labeled: true, max: 2000, step: 5},
                        labelProps: {
                            showTooltip: true
                        }
                    })
                    .value<number>(1200)
            )
            .add("budget", f =>
                f
                    .domain(DO_MONTANT)
                    .metadata({
                        label: "app.profile.budget",
                        comment: "Ce champ sert à démontrer la saisie d'un montant",
                        labelProps: {
                            showTooltip: true
                        }
                    })
                    .value<number>(123_456.7)
            )
            .patch("droitCodes", (f, node) =>
                f.metadata(() =>
                    node.nombreUtilisateursMax.value! < 1000
                        ? {SelectComponent: SelectChips<ZodArray<ZodString>>, selectProps: {hasSelectAll: true}}
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

    useReferenceTracking(actions.trackingId, referenceStore, "droit");

    return (
        <Form {...actions.formProps}>
            <Panel icon="settings" title="app.profile.detail" {...actions.panelProps}>
                {fieldFor(entity.libelle)}
                {selectFor(entity.droitCodes, referenceStore.droit)}
                {fieldFor(entity.nombreUtilisateursMax)}
                {fieldFor(entity.budget)}
                {!entity.form.isEdit ? fieldFor(entity.sourceNode.dateCreation) : null}
                {!entity.form.isEdit && entity.sourceNode.dateModification.value
                    ? fieldFor(entity.sourceNode.dateModification)
                    : null}
            </Panel>
        </Form>
    );
});
