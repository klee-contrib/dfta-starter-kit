export function getPhoto(file: Blob) {
    return new Promise<string>(resolve => {
        const fileReader = new FileReader();
        fileReader.readAsDataURL(file);
        function onLoad() {
            resolve(fileReader.result as string);
            fileReader.removeEventListener("load", onLoad);
        }
        fileReader.addEventListener("load", onLoad);
    });
}
