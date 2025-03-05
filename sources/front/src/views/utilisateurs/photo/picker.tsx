import {useObserver} from "mobx-react";
import {useId, useState} from "react";

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
                    label: "Enregistrer",
                    color: "primary"
                },
                {onClick: close, label: "Annuler", disabled: requestStore.isLoading(trackingId)}
            ]}
            active={active}
            onClosed={() => {
                setPhotoFile(undefined);
                setPhoto(undefined);
            }}
            onOverlayClick={close}
            title="Choississez une photo"
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
                {photo ? <img src={photo} /> : null}
            </div>
        </Dialog>
    ));
}
