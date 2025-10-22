import z from "zod";

import {domain, SelectCheckbox} from "@focus4/form-toolbox";

export const DO_BOOLEEN = domain(z.boolean());
export const DO_CODE = domain(z.string().max(10));
export const DO_CODE_LISTE = domain(z.array(z.string().max(10)), {
    SelectComponent: SelectCheckbox<z.ZodArray<z.ZodString>>
});
export const DO_DATE = domain(z.iso.date("focus.validation.date"), {
    inputProps: {
        inputProps: {icon: "calendar_month"}
    }
});
export const DO_DATE_HEURE = domain(z.iso.datetime());
export const DO_EMAIL = domain(z.email("focus.validation.email"), {inputProps: {icon: "email"}});
export const DO_ENTIER = domain(z.int("focus.validation.number"));
export const DO_ID = DO_ENTIER;
export const DO_LIBELLE = domain(z.string().max(100));
