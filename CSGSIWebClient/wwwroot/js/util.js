function convertFromUTC(utcDateTime) {
    const d = new Date(utcDateTime * 1000);
    return d.toGMTString();
}

function setUser(user) {
    let stringifiedUser = JSON.stringify(user);
    let encryptedUser = CryptoJS.AES.encrypt(stringifiedUser, secret);
    localStorage.setItem("user", encryptedUser);
}