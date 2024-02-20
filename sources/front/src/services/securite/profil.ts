////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

import {fetch} from "../../server";

import {ProfilItem} from "../../model/securite/profil-item";
import {ProfilRead} from "../../model/securite/profil-read";
import {ProfilWrite} from "../../model/securite/profil-write";

/**
 * Ajoute un Profil
 * @param profil Profil à sauvegarder
 * @param options Options pour 'fetch'.
 * @returns Profil sauvegardé
 */
export function addProfil(profil: ProfilWrite, options: RequestInit = {}): Promise<ProfilRead> {
    return fetch("POST", `./api/profils`, {body: profil}, options);
}

/**
 * Charge le détail d'un Profil
 * @param proId Id technique
 * @param options Options pour 'fetch'.
 * @returns Le détail de l'Profil
 */
export function getProfil(proId: number, options: RequestInit = {}): Promise<ProfilRead> {
    return fetch("GET", `./api/profils/${proId}`, {}, options);
}

/**
 * Liste tous les Profils
 * @param options Options pour 'fetch'.
 * @returns Profils matchant les critères
 */
export function getProfils(options: RequestInit = {}): Promise<ProfilItem[]> {
    return fetch("GET", `./api/profils`, {}, options);
}

/**
 * Sauvegarde un Profil
 * @param proId Id technique
 * @param profil Profil à sauvegarder
 * @param options Options pour 'fetch'.
 * @returns Profil sauvegardé
 */
export function updateProfil(proId: number, profil: ProfilWrite, options: RequestInit = {}): Promise<ProfilRead> {
    return fetch("PUT", `./api/profils/${proId}`, {body: profil}, options);
}
