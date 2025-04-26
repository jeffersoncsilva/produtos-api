
function mostrarPopUpBoostrap(msg) {
    const modalHtml = `
	<div class="modal fade" id="meuModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title text-center" id="exampleModalLabel">Aviso</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <p class="text-justify">${msg}</p>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fechar</button>
          </div>
        </div>
      </div>
    </div>
	`;

    // Adiciona o HTML do modal ao body

    const modalDiv = document.getElementById('modalHtml');

    if (!modalDiv)
        return;

    modalDiv.innerHTML = modalHtml;

    // Obtém a instância do modal Bootstrap
    const modalElement = document.getElementById('meuModal');
    if (modalElement) {
        const modalBootstrap = bootstrap.Modal.getOrCreateInstance(modalElement);
        modalBootstrap.show();
        modalElement.addEventListener('hidden.bs.modal', () => {
            modalDiv.innerHTML = '';
        });
    }
}

