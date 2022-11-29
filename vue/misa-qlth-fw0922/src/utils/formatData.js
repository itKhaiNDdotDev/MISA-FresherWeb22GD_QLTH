/**
 * Kiểm tra xem có phải số điện thoại hợp lệ hay không (đảm bảo đúng độ dài 7-20 ký tự số)
 * @param {string} phoneNumber Số điện thoại cần kiểm tra 
 * @returns true nếu là SĐT hợp lệ và false với trường hợp ngược lại
 * Author: KhaiND (26/11/2022)
 */
 export function isPhone(phoneNumber) {
    var isDigitString = phoneNumber.match(/\d+/) ? true : false;
    return isDigitString && phoneNumber.length >= 7 && phoneNumber.length <= 20;
}

/**
 * Kiêm tra xem email có đúng định dạng hợp lệ hay không
 * @param {string} email email cần kiểm tra
 * @returnstrue nếu là email hợp lệ và false với trường hợp ngược lại
 * Author: KhaiND (23/11/2022)
 */
export function isEmail(email) {
    const regexEmail = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    return email.match(regexEmail) ? true : false;
}

/**Tham khảo:
 * Kiểm tra xem có phải số điện thoại Việt Nam hợp lệ hay không
 * @param {string} phoneNumber Số điện thoại cần kiểm tra 
 * @returns true nếu là SĐT VN hợp lệ và false với trường hợp ngược lại
 * Author: KhaiND (23/11/2022)
 */
export function isVietnamesePhoneNumber(phoneNumber) {
    const regexPhoneNumber = /(84|0[3|5|7|8|9])+([0-9]{8})\b/g;
    return phoneNumber.match(regexPhoneNumber) ? true : false;
}