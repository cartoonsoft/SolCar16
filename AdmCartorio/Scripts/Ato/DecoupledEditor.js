/*************************************************************************************************
******************************* CRIANDO O EDITOR DE TEXTO WORD **********************************
*************************************************************************************************/
DecoupledEditor
    .create(document.querySelector('.document-editor__editable'), {
        cloudServices: {
            //
        },
        toolbar: ['alignment:justify', '|', 'Bold', 'Italic', 'Underline'],

    })
    .then(editor => {
        const toolbarContainer = document.querySelector('.document-editor__toolbar ');

        toolbarContainer.appendChild(editor.ui.view.toolbar.element);

        window.editor = editor;
    })
    .catch(err => {
        console.error(err);
    });
