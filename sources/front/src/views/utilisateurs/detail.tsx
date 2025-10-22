import {autorun} from "mobx";
import {useObserver} from "mobx-react";
import {useEffect, useId, useState} from "react";
import {useTranslation} from "react-i18next";

import {messageStore, requestStore} from "@focus4/core";
import {Display, SelectRadio} from "@focus4/form-toolbox";
import {autocompleteFor, fieldFor, Form, selectFor, useFormActions, useFormNode, useLoad} from "@focus4/forms";
import {Dialog, Panel} from "@focus4/layout";
import {makeReferenceList, toFlatValues} from "@focus4/stores";
import {FontIcon} from "@focus4/toolbox";

import {getProfils} from "../../services/securite/profil";
import {
    addUtilisateur,
    addUtilisateurPhoto,
    deleteUtilisateurPhoto,
    getUtilisateur,
    getUtilisateurPhoto,
    updateUtilisateur
} from "../../services/securite/utilisateur";
import {profilStore} from "../../stores/profil";
import {referenceStore} from "../../stores/references";
import {utilisateurStore} from "../../stores/utilisateur";

import {DO_LIBELLE} from "../../domains";
import {router} from "../../router";
import {fetch} from "../../server";

import {PhotoDisplay} from "./photo/display";
import {PhotoPicker} from "./photo/picker";
import {getPhoto} from "./photo/utils";

import css from "./__style__/detail.css";

export function UtilisateurDetail({closePopin}: {closePopin?: () => void}) {
    const {t} = useTranslation();

    const [photoDialogActive, setPhotoDialogActive] = useState(false);
    const [photoDialogDeleteActive, setPhotoDialogDeleteActive] = useState(false);
    const deletingId = useId();

    const entity = useFormNode(utilisateurStore.utilisateur, e =>
        (!router.state.utilisateurs.utiId ? e.edit(() => true) : e)
            .remove("id", "dateCreation", "dateModification")
            .patch("profilId", f =>
                f.metadata({
                    DisplayComponent: props => (
                        <a className={css.link} href={router.href(x => x("profils")(props.value!))}>
                            <Display {...props} />
                            <FontIcon>open_in_new</FontIcon>
                        </a>
                    )
                })
            )
            .patch("typeUtilisateurCode", f => f.metadata({SelectComponent: SelectRadio<"string">}))
            .add("photo", f =>
                f
                    .edit(false)
                    .domain(DO_LIBELLE)
                    .metadata({
                        label: "app.user.picture.title",
                        DisplayComponent: PhotoDisplay,
                        displayProps: {
                            openPicker: () => setPhotoDialogActive(true),
                            openDelete: () => setPhotoDialogDeleteActive(true)
                        }
                    })
            )
    );

    useLoad(profilStore.profils, a => a.params().load(getProfils));

    const actions = useFormActions(entity, a =>
        a
            .init()
            .params(() => router.state.utilisateurs.utiId)
            .load(getUtilisateur)
            .create(addUtilisateur)
            .update(updateUtilisateur)
            .withConfirmation(router)
            .on(["create", "cancel"], () => closePopin?.())
            .on("create", (_, uti) => router.to(x => x("utilisateurs")(uti.id!)))
    );

    useEffect(
        () =>
            autorun(() => {
                const {utiId} = router.state.utilisateurs;
                if (utiId) {
                    requestStore.track(actions.trackingId, () =>
                        getUtilisateurPhoto(utiId).then(async response => {
                            entity.photo.value = response ? await getPhoto(await response.blob()) : undefined;
                        })
                    );
                }
            }),
        []
    );

    return useObserver(() => (
        <Form {...actions.formProps}>
            <Panel
                title={t(actions.params ? "app.user.detail.consult" : "app.user.detail.create")}
                {...actions.panelProps}
            >
                {actions.params ? fieldFor(entity.photo) : null}
                {fieldFor(entity.nom)}
                {fieldFor(entity.prenom)}
                {fieldFor(entity.email)}
                {fieldFor(entity.dateNaissance)}
                {autocompleteFor(entity.adresse, {
                    keyResolver: async label => label,
                    querySearcher: query => searchAdresse(query),
                    autocompleteProps: {icon: "place"}
                })}
                {fieldFor(entity.actif)}
                {selectFor(entity.typeUtilisateurCode, referenceStore.typeUtilisateur)}
                {selectFor(
                    entity.profilId,
                    makeReferenceList(toFlatValues(profilStore.profils), {valueKey: "id", labelKey: "libelle"})
                )}
                {!entity.form.isEdit ? fieldFor(entity.sourceNode.dateCreation) : null}
                {!entity.form.isEdit && entity.sourceNode.dateModification.value
                    ? fieldFor(entity.sourceNode.dateModification)
                    : null}
            </Panel>
            <PhotoPicker
                active={photoDialogActive}
                close={() => setPhotoDialogActive(false)}
                onPick={async (file, photo) => {
                    await addUtilisateurPhoto(...actions.params!, file);
                    messageStore.addSuccessMessage("app.user.picture.update");
                    entity.photo.value = photo;
                }}
            />
            <Dialog
                actions={[
                    {
                        onClick: async () => {
                            await requestStore.track(deletingId, () => deleteUtilisateurPhoto(...actions.params!));
                            messageStore.addSuccessMessage("La photo a bien été supprimée");
                            entity.photo.value = undefined;
                            setPhotoDialogDeleteActive(false);
                        },
                        label: "Supprimer",
                        color: "primary",
                        loading: requestStore.isLoading(deletingId),
                        disabled: requestStore.isLoading(deletingId)
                    },
                    {
                        onClick: () => setPhotoDialogDeleteActive(false),
                        label: "Annuler",
                        disabled: requestStore.isLoading(deletingId)
                    }
                ]}
                active={photoDialogDeleteActive}
                title="Êtes-vous sûr de vouloir supprimer la photo"
            >
                Vous pourrez en re-télécharger une par la suite.
            </Dialog>
        </Form>
    ));
}

function searchAdresse(query: string): Promise<{key: string; label: string}[]> {
    return fetch("GET", `./api/adresses?query=${encodeURIComponent(query)}`);
}
