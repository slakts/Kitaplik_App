// Genel tablo arama fonksiyonu
function searchTable(inputId, tableId, columnIndex) {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById(inputId);
    filter = input.value.toUpperCase();
    table = document.getElementById(tableId);
    tr = table.getElementsByTagName("tr");

    for (i = 1; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[columnIndex];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

// Satır seçme efekti
function selectRow(row) {
    let selected = document.querySelector(".selected-row");
    if (selected) {
        selected.classList.remove("selected-row");
    }
    row.classList.add("selected-row");
}

// CSS ile seçilen satırın rengini ayarla
document.addEventListener("DOMContentLoaded", function () {
    const style = document.createElement("style");
    style.innerHTML = `
        .selected-row {
            background-color: #D2A679 !important;
            color: white;
            font-weight: bold;
        }
    `;
    document.head.appendChild(style);
});
