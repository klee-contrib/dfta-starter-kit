import {useObserver} from "mobx-react";
import {useId, useState} from "react";

import {requestStore} from "@focus4/core";
import {Dialog} from "@focus4/layout";
import {TextField} from "@focus4/toolbox";

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
                <TextField
                    accept="image/jpeg"
                    onChange={(_, e) => {
                        const file = (e.target as HTMLInputElement).files?.[0];
                        if (file) {
                            setPhotoFile(file);
                            const fileReader = new FileReader();
                            fileReader.readAsDataURL(file);
                            function onLoad() {
                                setPhoto(fileReader.result as string);
                                fileReader.removeEventListener("load", onLoad);
                            }
                            fileReader.addEventListener("load", onLoad);
                        }
                    }}
                    type="file"
                />
                {photo ? <img src={photo} /> : null}
            </div>
        </Dialog>
    ));
}
