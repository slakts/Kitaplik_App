// Satır seçme efekti
function selectRow(row) {
    let selected = document.querySelector(".selected-row");
    if (selected) {
        selected.classList.remove("selected-row");
    }
    row.classList.add("selected-row");
}

// CSS ile seçilen satırın arka plan rengini değiştirme
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
