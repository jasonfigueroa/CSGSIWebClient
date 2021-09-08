function convertFromUTC(utcDateTime) {
    const d = new Date(utcDateTime * 1000);
    return d.toGMTString();
}

function setUser(user) {
    let stringifiedUser = JSON.stringify(user);
    let encryptedUser = CryptoJS.AES.encrypt(stringifiedUser, secret);
    localStorage.setItem("user", encryptedUser);
}

function getUser() {
    let stringifiedUser = localStorage.getItem("user");
    let decryptedUser = CryptoJS.AES.decrypt(stringifiedUser, secret);
    let stringifiedDecryptedUser = decryptedUser.toString(CryptoJS.enc.Utf8);
    return JSON.parse(stringifiedDecryptedUser);
}
