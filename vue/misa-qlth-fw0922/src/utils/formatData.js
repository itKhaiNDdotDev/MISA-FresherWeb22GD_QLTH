export function isVietnamesePhoneNumber(phoneNumber) {
    const regexPhoneNumber = /(84|0[3|5|7|8|9])+([0-9]{8})\b/g;
    return phoneNumber.match(regexPhoneNumber) ? true : false;
}

export function isEmail(email) {
    const regexEmail = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    return email.match(regexEmail) ? true : false;
}   