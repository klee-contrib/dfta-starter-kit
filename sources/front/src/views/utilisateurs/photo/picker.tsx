import {useObserver} from "mobx-react";
import {useId, useState} from "react";
import {useTranslation} from "react-i18next";

import {requestStore} from "@focus4/core";
import {InputFile} from "@focus4/form-toolbox";
import {Dialog} from "@focus4/layout";

import css from "./__style__/photo.css";

export function PhotoPicker({
    active,
    close,
    onPick
}: {
    active: boolean;
    close: () => void;
    onPick: (file: File, photo: string) => Promise<void>;
}) {
    const {t} = useTranslation();
    const trackingId = useId();
    const [photoFile, setPhotoFile] = useState<File>();
    const [photo, setPhoto] = useState<string>();

    return useObserver(() => (
        <Dialog
            actions={[
                {
                    onClick: async () => {
                        await requestStore.track(trackingId, () => onPick(photoFile!, photo!));
                        close();
                    },
                    disabled: !photoFile || !photo || requestStore.isLoading(trackingId),
                    loading: requestStore.isLoading(trackingId),
                    label: t("focus.button.save"),
                    color: "primary"
                },
                {onClick: close, label: t("focus.button.cancel"), disabled: requestStore.isLoading(trackingId)}
            ]}
            active={active}
            onClosed={() => {
                setPhotoFile(undefined);
                setPhoto(undefined);
            }}
            onOverlayClick={close}
            title={t("app.user.picture.choose")}
        >
            <div className={css.picker}>
                <InputFile
                    accept="image/*"
                    maxFiles={1}
                    name="file"
                    onChange={file => {
                        setPhotoFile(file);
                        if (file) {
                            const fileReader = new FileReader();
                            fileReader.readAsDataURL(file);
                            function onLoad() {
                                setPhoto(fileReader.result as string);
                                fileReader.removeEventListener("load", onLoad);
                            }
                            fileReader.addEventListener("load", onLoad);
                        } else {
                            setPhoto(undefined);
                        }
                    }}
                />
                {photo ? <img alt={t("app.user.picture.title")} src={photo} /> : null}
            </div>
        </Dialog>
    ));
}
