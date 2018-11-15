(function () {

    var table = document.querySelector("#tab");
    var ths = table.querySelectorAll("thead th");
    var trs = table.querySelectorAll("tbody tr");
    var df = document.createDocumentFragment();


    function makeArray(nodeList) {
        var arr = [];
        for (var i = 0; i < nodeList.length; i++) {
            arr.push(nodeList[i]);
        }
        return arr;
    }

    function clearClassName(nodeList) {
        for (var i = 0; i < nodeList.length; i++) {
            nodeList[i].className = "";
        }

    }

    function sortBy(e) {
        var target = e.target;
        var thsArr = makeArray(ths);
        var trsArr = makeArray(trs)
        var index = thsArr.indexOf(target);
        // var order=(target.classList.contains("") || target.classList.contains("desc"))?"asc":"desc";
        var order = (target.className === "" || target.className === "desc") ? "asc" : "desc";
        console.log(order);
        clearClassName(ths);
        trsArr.sort(function (a, b) {

            var tdA = a.children[index].textContent;
            var tdB = b.children[index].textContent;
            if (tdA < tdB) {
                return order === "asc" ? -1 : 1;
            } else if (tdA > tdB) {
                return order === "asc" ? 1 : -1;
            } else {
                return 0;
            }
        });
        // console.log(trsArr);
        trsArr.forEach(function (element) {
            df.appendChild(element)
        });
        // console.log(df);

        table.querySelector("tbody").appendChild(df);
        // target.classList.add(order);
        target.className = order;
    }

    for (var i = 0; i < ths.length; i++) {
        ths[i].addEventListener("click", sortBy);
    }

})();