$(document).ready(function () {
    // DROPDOWN-LIST
    selectDropdown("#ddlToBoMon");
    // COMBO-BOX
    selectCombobox("#cbxQLTheoMon");
    selectCombobox("#cbxQLTheoKhoPhong")
});

/**
 * Feat: Hàm thực hiện việc click để ẩn, hiển và lựa chọn các item trong dropdown
 * @param {Selector} dropdownId string trỏ đế id selector của dropdown tương ứng
 * Author: KhaiND (24/10/2022)
 */
function selectDropdown(dropdownId) {
    try {
        $(dropdownId + " input").click(function () {
            console.log("Click DD");
            $(this).siblings(".dropdown__option").toggle();
        });
        // Lựa chọn item khi click vào item tương ứng của dropdown
        $(dropdownId + " .dropdown__option--item").click(function () {
            $(this).parent().siblings("input").val($(this).text());
            $(this).parent().hide();
        });
        // Kiểm tra nếu bấm vào vùng ngoài vùng lựa chọn của dropdown thì ẩn đi và không thay đổi lựa chọn
        //...........
    } catch (error) {
        console.log(error);
    }
}

/**
 * Feat: Hàm thực hiện việc ẩn, hiện option để lựa chọn và append các tag được chọn cho dropdownlist multiple-choice/combobox
 * @param {selector} comboboxId string trỏ đến Id Selector của combobox tương ứng
 * Author: KhaiND (24/10/2022)
 */
function selectCombobox(comboboxId) {
    try {
        $(comboboxId + "Res").click(togHandler = function () {
            console.log("Click CB");
            $(this).siblings(".dropdown__option").toggle();
            //Bấm x để bỏ tag tương ứng đi
            $(".icon-x").unbind("click", togHandler);
            $(".icon-x").click(function () {
                console.log(this);
                $(this).parent().remove();
            });
        });
        // Lựa chọn item khi click vào item tương ứng của dropdown
        $(comboboxId + " .dropdown__option--item").click(function () {
            // // Tick vào checkbox tương ứng item
            // console.log($(this).children(".item--check .checkbox-real"));
            // $(this).children(".item--check").children(".checkbox-real").css("opacity","100%");
            var checkStatus = $(this).children(".item--check").children(".checkbox-real").is(':checked');
            $(this).children(".item--check").children(".checkbox-real").prop('checked', !checkStatus);
            // Append item dạng thẻ tag lên tag-list
            if ($(this).children(".item--check").children(".checkbox-real").is(':checked'))
            {
                var apTag = $(`
                    <div class="m-tag">
                        <span class="m-tag-text">${$(this).children(".item--text").text()}</span>
                        <div class="m-icon m-icon-24 icon-x"></div>
                    </div>
                `);
                $(comboboxId + "Res").append(apTag);
            }
            else {  // Uncheck thì bỏ tag đi
                // var txt = $(this).children(".item--text").text();
                // var popTagText = $(comboboxId + "Res").children(".m-tag").children(".m-tag--text:contains(txt)");
                // console.log($(comboboxId + "Res").children(".m-tag").children(".m-tag--text").text() + "----AND----" + $(this).children(".item--text").text())
                // console.log(popTagText);
            }
        });
    } catch (error) {
        console.log(error);
    }
}