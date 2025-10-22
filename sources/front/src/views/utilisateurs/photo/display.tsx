import {useTranslation} from "react-i18next";
import z from "zod";

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
    const {t} = useTranslation();
    return (
        <span className={css.display}>
            {value ? (
                <img alt={t("app.user.picture.title")} src={value} />
            ) : (
                <Display schema={z.string()} value={t("app.user.picture.none")} />
            )}
            <span>
                {openPicker ? (
                    <Tooltip tooltip={t("app.user.picture.upload")}>
                        <IconButton icon="add_a_photo" onClick={openPicker} />
                    </Tooltip>
                ) : null}
                {value ? (
                    <Tooltip tooltip={t("app.user.picture.delete")}>
                        <IconButton icon="delete" onClick={openDelete} />
                    </Tooltip>
                ) : null}
            </span>
        </span>
    );
}
