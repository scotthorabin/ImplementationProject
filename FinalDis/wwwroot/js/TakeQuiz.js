window.addEventListener('load', function () {
    const quizForm = document.querySelector('.quiz');

    if (quizForm) {
        quizForm.addEventListener('submit', function (e) {
            e.preventDefault(); // Stop form from submitting straight away

            Swal.fire({
                title: 'Are you sure?',
                text: "Do you want to submit your answers?",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, submit!',
                cancelButtonText: 'Cancel'
            }).then((result) => {
                if (result.isConfirmed) {
                    quizForm.submit(); // Submit the form if confirmed
                }
            });
        });
    }
});
