////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

import fetch from "../../server";

import {TypeUtilisateurCode} from "../../model/securite/references";
import {UtilisateurItem} from "../../model/securite/utilisateur/utilisateur-item";
import {UtilisateurRead} from "../../model/securite/utilisateur/utilisateur-read";
import {UtilisateurWrite} from "../../model/securite/utilisateur/utilisateur-write";

/**
 * Ajoute un utilisateur
 * @param utilisateur Utilisateur à sauvegarder
 * @param options Options pour 'fetch'.
 * @returns Utilisateur sauvegardé
 */
export async function addUtilisateur(utilisateur: UtilisateurWrite, options: RequestInit = {}): Promise<UtilisateurRead> {
    const response = await fetch(`./api/utilisateurs`, {
        ...options,
        method: "POST",
        body: JSON.stringify(utilisateur),
        headers: {...options.headers, "Content-Type": "application/json"}
    });
    return await response.json();
}

/**
 * Ajoute une nouvelle photo pour un utilisateur.
 * @param utiId Id de l'utilisateur
 * @param photo Photo.
 * @param options Options pour 'fetch'.
 */
export async function addUtilisateurPhoto(utiId: number, photo: File, options: RequestInit = {}): Promise<void> {
    const body = new FormData();
    fillFormData(
        {
            photo
        },
        body
    );
    await fetch(`./api/utilisateurs/${utiId}/photo`, {
        ...options,
        method: "PUT",
        body
    });
}

/**
 * Supprime un utilisateur
 * @param utiId Id de l'utilisateur
 * @param options Options pour 'fetch'.
 */
export async function deleteUtilisateur(utiId: number, options: RequestInit = {}): Promise<void> {
    await fetch(`./api/utilisateurs/${utiId}`, {
        ...options,
        method: "DELETE"
    });
}

/**
 * Supprime la photo d'un utilisateur
 * @param utiId Id de l'utilisateur
 * @param options Options pour 'fetch'.
 */
export async function deleteUtilisateurPhoto(utiId: number, options: RequestInit = {}): Promise<void> {
    await fetch(`./api/utilisateurs/${utiId}/photo`, {
        ...options,
        method: "DELETE"
    });
}

/**
 * Charge le détail d'un utilisateur
 * @param utiId Id de l'utilisateur
 * @param options Options pour 'fetch'.
 * @returns Le détail de l'utilisateur
 */
export async function getUtilisateur(utiId: number, options: RequestInit = {}): Promise<UtilisateurRead> {
    const response = await fetch(`./api/utilisateurs/${utiId}`, {
        ...options,
        method: "GET"
    });
    return await response.json();
}

/**
 * Charge la photo d'un utilisateur (si elle existe).
 * @param utiId Id de l'utilisateur
 * @param options Options pour 'fetch'.
 * @returns Photo.
 */
export async function getUtilisateurPhoto(utiId: number, options: RequestInit = {}): Promise<Response | undefined> {
    const response = await fetch(`./api/utilisateurs/${utiId}/photo`, {
        ...options,
        method: "GET"
    });
    if (response.status === 204) {
        return undefined;
    }
    return response;
}

/**
 * Recherche des utilisateurs
 * @param nom Nom de l'utilisateur
 * @param prenom Nom de l'utilisateur
 * @param email Email de l'utilisateur
 * @param dateNaissance Age de l'utilisateur
 * @param actif Si l'utilisateur est actif
 * @param profilId Profil de l'utilisateur
 * @param typeUtilisateurCode Type d'utilisateur
 * @param options Options pour 'fetch'.
 * @returns Utilisateurs matchant les critères
 */
export async function searchUtilisateur(nom?: string, prenom?: string, email?: string, dateNaissance?: string, actif?: boolean, profilId?: number, typeUtilisateurCode?: TypeUtilisateurCode, options: RequestInit = {}): Promise<UtilisateurItem[]> {
    const query = new URLSearchParams();
    if (nom !== undefined) {
        query.append("nom", nom)
    }
    if (prenom !== undefined) {
        query.append("prenom", prenom)
    }
    if (email !== undefined) {
        query.append("email", email)
    }
    if (dateNaissance !== undefined) {
        query.append("dateNaissance", dateNaissance)
    }
    if (actif !== undefined) {
        query.append("actif", `${actif}`)
    }
    if (profilId !== undefined) {
        query.append("profilId", `${profilId}`)
    }
    if (typeUtilisateurCode !== undefined) {
        query.append("typeUtilisateurCode", typeUtilisateurCode)
    }
    const response = await fetch(`./api/utilisateurs?${query}`, {
        ...options,
        method: "GET"
    });
    return await response.json();
}

/**
 * Sauvegarde un utilisateur
 * @param utiId Id de l'utilisateur
 * @param utilisateur Utilisateur à sauvegarder
 * @param options Options pour 'fetch'.
 * @returns Utilisateur sauvegardé
 */
export async function updateUtilisateur(utiId: number, utilisateur: UtilisateurWrite, options: RequestInit = {}): Promise<UtilisateurRead> {
    const response = await fetch(`./api/utilisateurs/${utiId}`, {
        ...options,
        method: "PUT",
        body: JSON.stringify(utilisateur),
        headers: {...options.headers, "Content-Type": "application/json"}
    });
    return await response.json();
}

function fillFormData(data: any, formData: FormData, prefix = "") {
    if (Array.isArray(data)) {
        for (const [i, item] of data.entries()) {
            fillFormData(item, formData, prefix + (typeof item === "object" && !(item instanceof File) ? `[${i}]` : ""));
        }
    } else if (typeof data === "object" && !(data instanceof File)) {
        for (const key in data) {
            fillFormData(data[key], formData, (prefix ? `${prefix}.` : "") + key);
        }
    } else {
        formData.append(prefix, data);
    }
}
