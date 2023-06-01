// DECLARACION DE VARIABLES
let $departamentosSelect = document.getElementById('departamento');
let $paisSelect = document.getElementById('pais');

document.addEventListener("DOMContentLoaded", () => {
    cargarPais();
})

$paisSelect.addEventListener("change", (event) => {
    let { value } = event.target;

    value = value.substring(0, value.indexOf("-"));

    cargarEstados(value)
})

const cargarPais = () => {
    const url = "https://api.countrystatecity.in/v1/countries/";

    Ajax({
        url,
        cbSuccess: (data) => {
            const fragment = document.createDocumentFragment();

            $paisSelect.innerHTML = "<option>Selecciona un pais...</option>";

            data.forEach((pais) => {
                const option = document.createElement("option");

                option.value = pais.iso2 +'-'+pais.name;
                option.textContent = pais.name;

                fragment.appendChild(option);
            });

            $paisSelect.appendChild(fragment)

        }
    });

}

const cargarEstados = (pais) => {
    const url = `https://api.countrystatecity.in/v1/countries/${pais}/states`;

    Ajax({
        url,
        cbSuccess: (data) => {
            const fragment = document.createDocumentFragment();

            $departamentosSelect.innerHTML = "<option>Selecciona un departamento...</option>";

            data.forEach((departamento) => {
                const option = document.createElement("option");

                option.value = departamento.name;
                option.textContent = departamento.name;

                fragment.appendChild(option);
            });

            $departamentosSelect.appendChild(fragment)
        }
    });
}