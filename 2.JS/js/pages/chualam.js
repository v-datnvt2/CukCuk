function listenFilter() {
    $(".toolbar .filter #searchfield").on("input", function() {
        filterEmployee();
    });
    $('.toolbar .filter .divtext').bind('contentchanged', function() {
        filterEmployee();
        // alert("whoo")
    });
}


function filterEmployee() {
    var keyw = $(".toolbar .filter #searchfield").val().toLowerCase(),
        division = $(".toolbar .filter .division .divtext").text(),
        position = $(".toolbar .filter .position .divtext").text(),
        bykey = keyw.length > 0,
        bydivision = division.search("Tất cả") < 0,
        byposition = position.search("Tất cả") < 0,
        filteredList = [];
    console.log("Rule1 :", keyw, division, position)
    if (!bydivision) division = "Phòng";
    if (!byposition) position = "Phòng";
    // console.log("Rule:", bykey, bydivision, byposition)
    if (!bykey && !bydivision && !byposition) {
        filteredList = employeeList;
    } else {
        employeeList.forEach((item, index) => {
            let info = (item.EmployeeCode + " " + item.FullName + " " + item.PhoneNumber).toLowerCase();
            if (bykey) {
                if (info.search(keyw) >= 0 && (item.DepartmentName + "aaa").toLowerCase().search(division) >= 0 &&
                    (item.PositionName + "aaa").toLowerCase().search(position) >= 0) {
                    console.log("By Key Item: ", index, matchKey);
                    filteredList.push(item);
                }
            } else {
                if ((item.DepartmentName + " aaa").toLowerCase().search(division) >= 0 &&
                    (item.PositionName + " aaa").toLowerCase().search(position) >= 0) {
                    console.log("Nokey Item: ", index, matchKey);
                    filteredList.push(item);
                }
            }

        });

    }
    loadTableEmployee(filteredList);
}