import {Display} from "@focus4/form-toolbox";
import {IconButton, Tooltip} from "@focus4/toolbox";

import css from "./__style__/photo.css";

export function PhotoDisplay({
    openDelete,
    openPicker,
    value
}: {
    openDelete?: () => void;
    openPicker?: () => void;
    value?: string;
}) {
    return (
        <span className={css.display}>
            {value ? <img alt="Profil" src={value} /> : <Display type="string" value="Pas de photo" />}
            <span>
                {openPicker ? (
                    <Tooltip tooltip="Télécharger une photo">
                        <IconButton icon="add_a_photo" onClick={openPicker} />
                    </Tooltip>
                ) : null}
                {value ? (
                    <Tooltip tooltip="Supprimer la photo">
                        <IconButton icon="delete" onClick={openDelete} />
                    </Tooltip>
                ) : null}
            </span>
        </span>
    );
}
