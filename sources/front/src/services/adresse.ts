import fetch from "../server";

/**
 * Récupère une adresse par sa clé.
 * @param key Clé.
 * @param options Options pour 'fetch'.
 * @returns Libellé de l'adresse.
 */
export function getAdresseLabel(key: string, options: RequestInit = {}): Promise<string> {
    return fetch("./api/adresse", {...options, searchParams: {key}}).text();
}

/**
 * Recherche les adresses.
 * @param query Query.
 * @param options Options pour 'fetch'.
 * @returns Adresses.
 */
export function searchAdresse(query: string, options: RequestInit = {}): Promise<{key: string; label: string}[]> {
    return fetch("./api/adresses", {...options, searchParams: {query}}).json();
}
