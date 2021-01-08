$(function () {
    //Assegnazione eventi
    $('#addResource').on('click', InitResourceModal);
    $('#addLesson').on('click', InitLessonModal);
    $('#addCourse').on('click', InitCourseModal);
    $('#addRoom').on('click', InitRoomModal);
    $('#saveLesson').on('click', SaveLesson);
    $('#saveResource').on('click', SaveResource);
    $('#saveCourse').on('click', SaveCourse);
    $('#saveRoom').on('click', SaveRoom);
    $('#lessonAddStudent').on('click', AddStudentToList);
    $('#lessonRemoveStudent').on('click', RemoveStudentFromList);
    $('#removeElement').on('click', RemoveElement);
    $('#lessonStart').on('change', SetLessonEndMin);
    $('#courseStart').on('change', SetCourseEndMin);
    //Carico di default la homepage
    ShowSection(0);
});
//SctionIds -> Variabile usata nella funzione "ShowSection" per caricare la sezione selezionata
let sectionIds = ['#HomeDiv', '#ResourcesDiv', '#LessonsDiv', '#CoursesDiv', '#RoomsDiv'];
//ActiveSection -> Variabile che indica quale sezione è visualizzata
let ActiveSection;
function ShowSection(arrayIndex) {
    for (let i = 0; i < sectionIds.length; i++) {
        if (i === arrayIndex) {
            $(sectionIds[i]).show();
            switch (sectionIds[i]) {
                case '#ResourcesDiv':
                    LoadResources();
                    break;
                case '#LessonsDiv':
                    LoadLessons();
                    break;
                case '#CoursesDiv':
                    LoadCourses();
                    break;
                case '#RoomsDiv':
                    LoadRooms();
                    break;
            }
            ActiveSection = sectionIds[i];
        }
        else {
            $(sectionIds[i]).hide();
        }
    }
    //Assegno l'evento di KeyUp alla barra di ricerca della sezione caricata
    $('input[name="tableSearch"]').on('keyup', Search);
    //La pulisco in modo tale da non avere filtri precedenti attivi
    $('input[name="tableSearch"]').val('');
    Search();
}
//Funzione per la gestione della ricerca
function Search() {
    if (ActiveSection === "#HomeDiv")
        return;
    var text = $(`${ActiveSection} input[name="tableSearch"]`).val().toString().toLowerCase();
    var txtValue = "";
    var find = false;
    for (var i = 0; i < $(`${ActiveSection} .tableBody tr`).length; i++) {
        txtValue = $(`${ActiveSection} .tableBody tr`)[i].textContent.toLowerCase();
        if (txtValue.indexOf(text) > -1) {
            $(`${ActiveSection} .tableBody tr`)[i].style.display = "";
        }
        else {
            $(`${ActiveSection} .tableBody tr`)[i].style.display = "none";
        }
    }
}
//Funzioni di caricamento degli elementi
function LoadResources() {
    $.getJSON('http://localhost:49267/api/Resources/GetAll')
        .done(function (resources) {
        $('#ResourcesTable').empty();
        for (let res of resources) {
            $("#ResourcesTable").append(`<tr> \
            <td>${res.id}</td> \
            <td>${res.lastName}</td> \
            <td>${res.name}</td> \
            <td> \
            <button type="button" value=${res.id} class="btn btn-primary resourceDetail" title="Dettagli"><i class="fas fa-fw fa-eye"></i></button>\
            </td>\
            </tr>`);
        }
        //Dopo il caricamento aggancio ai pulsanti presenti nella tabella l'evento di click
        $('.resourceDetail').on('click', function (e) {
            OpenResourceDetail(parseInt($(this).attr("value")));
        });
    });
}
function LoadLessons() {
    $.getJSON('http://localhost:49267/api/Lessons/GetAll')
        .done(function (lessons) {
        $('#LessonsTable').empty();
        for (let lesson of lessons) {
            $("#LessonsTable").append(`<tr> \
            <td>${lesson.lessonNumber}</td> \
            <td>${lesson.courseName}</td> \
            <td>${lesson.teacher}</td> \
            <td>${lesson.roomName}</td> \
            <td>\
            <button type="button" value=${lesson.id} class="btn btn-primary lessonDetail" title="Dettagli"><i class="fas fa-fw fa-eye"></i></button> \
            <button type="button" value=${lesson.id} class="btn btn-primary lessonRemove" title="Rimuovi"><i class="fas fa-fw fa-trash"></i></button> \
            </td>/tr>`);
        }
        $('.lessonDetail').on('click', function (e) {
            OpenLessonDetail(parseInt($(this).attr("value")));
        });
        $('.lessonRemove').on('click', function (e) {
            RemoveLesson(parseInt($(this).attr("value")));
        });
    });
}
function LoadCourses() {
    $.getJSON('http://localhost:49267/api/Courses/GetAll')
        .done(function (courses) {
        $('#CoursesTable').empty();
        for (let couse of courses) {
            $("#CoursesTable").append(`<tr> \
            <td>${couse.description}</td> \
            <td>${couse.referent}</td> \
            <td>${couse.referenceYear}</td> \
            <td>\
            <button type="button" value=${couse.id} class="btn btn-primary courseDetail" title="Dettagli"><i class="fas fa-fw fa-eye"></i></button> \
            <button type="button" value=${couse.id} class="btn btn-primary courseRemove" title="Rimuovi"><i class="fas fa-fw fa-trash"></i></button> \
            </td>/tr>`);
        }
        $('.courseDetail').on('click', function (e) {
            OpenCourseDetail(parseInt($(this).attr("value")));
        });
        $('.courseRemove').on('click', function () {
            RemoveCourse(parseInt($(this).attr('value')));
        });
    });
}
function LoadRooms() {
    $.getJSON('http://localhost:49267/api/Rooms/GetAll')
        .done(function (rooms) {
        $('#RoomsTable').empty();
        for (let room of rooms) {
            $("#RoomsTable").append(`<tr> \
            <td>${room.roomNumber}</td> \
            <td>${room.name}</td> \
            <td><i id="${room.id}"></i></td> \
            <td>\
            <button type="button" value=${room.id} class="btn btn-primary roomDetail" title="Dettagli"><i class="fas fa-fw fa-eye"></i></button> \
            <button type="button" value=${room.id} class="btn btn-primary roomRemove" title="Rimuovi"><i class="fas fa-fw fa-trash"></i></button> \
            </td>/tr>`);
            if (room.bookable) {
                $(`#${room.id}`).addClass('fas fa-fw fa-check-circle');
            }
            else {
                $(`#${room.id}`).addClass('fas fa-fw fa-times-circle');
            }
        }
        $('.roomDetail').on('click', function (e) {
            OpenRoomDetail(parseInt($(this).attr("value")));
        });
        $('.roomRemove').on('click', function (e) {
            RemoveRoom(parseInt($(this).attr("value")));
        });
    });
}
//Funzioni per l'inizializzazione delle finestre modali di ogni elemento
function InitResourceModal() {
    $('#resourceForm :input').prop('readonly', false);
    $('#resourceForm :input').val('');
    $('#resourceModalTitle').text('Inserimento Nuova Risorsa');
    $('#saveResource').show();
    $('#resourceEmailField').hide();
    $('#resourceIdField').hide();
    $('#resourceUsernameField').hide();
}
function InitLessonModal() {
    $('#lessonStudents').empty();
    $('#globalResources').empty();
    $('#lessonNumber').val('');
    $('#lessonStart').val('');
    $('#lessonEnd').val('');
    $('#lessonId').val(0);
    $('#lessonModalTitle').text('Inserimento Nuova Lezione');
    LoadLessonModalSelect();
}
function InitCourseModal() {
    $('#courseForm :input').prop('readonly', false);
    $('#courseReferent').prop('disabled', false);
    $('#courseForm :input').val('');
    $('#saveCourse').show();
    $('#courseReferent').prop('readonly', false);
    $('#lessonModalTitle').text('Inserimento Nuovo Percorso');
    $('#courseId').val(0);
    LoadResourcesSelect('#courseReferent', true);
}
function InitRoomModal() {
    $('#roomForm :input').prop('readonly', false);
    $('#roomForm :input').val('');
    $('#saveRoom').show();
    $('#roomBookable').prop('disabled', false);
    $('#roomBookable').prop('checked', false);
    $('#roomModalTitle').text('Inserimento Nuova Sala');
}
function ShowDeleteModal() {
    $('#deleteModalTitle').text('Conferma Eliminazione');
    $('#removeElement').show();
    $('#removeDialogText').text('Eliminare l\'elemento selezionato?');
    $('#closeRemoveModal').text('No');
    $('#deleteModal').modal('show');
}
//Funzioni per la gestione dell'apertura dei dettagli degli elementi
function OpenResourceDetail(Id) {
    //Apro la finestra modale per la visualizzazione dei dettagli delle risorse
    $.getJSON(`http://localhost:49267/api/Resources/GetById/${Id}`)
        .done(function (resource) {
        $('#resourceForm :input').prop('readonly', true);
        // $('#ResourceId').prop('readonly',true);
        // $('#ResourceName').prop('readonly',true);
        // $('#ResourceLastName').prop('readonly',true);
        // $('#ResourceUsername').prop('readonly',true);
        // $('#ResourceEmail').prop('readonly',true);
        $('#resourceModalTitle').text('Dettagli Risorsa');
        $('#ResourceId').val(resource.id);
        $('#ResourceName').val(resource.name);
        $('#ResourceLastName').val(resource.lastName);
        $('#ResourceUsername').val(resource.username);
        $('#ResourceEmail').val(resource.email);
        $('#resourceEmailField').show();
        $('#resourceIdField').show();
        $('#resourceUsernameField').show();
        $('#saveResource').hide();
        $('#resourceModal').modal('show'); //Cast ad any per evitare errori di compilazione di typescript.
    });
}
function OpenLessonDetail(Id) {
    $.getJSON(`http://localhost:49267/api/Lessons/GetById/${Id}`)
        .done(function (lesson) {
        $('#lessonModalTitle').text('Modifica Lezione');
        $('#lessonStudents').empty();
        $('#globalResources').empty();
        $('#lessonNumber').val(lesson.lessonNumber);
        $('#lessonStart').val(lesson.startDate);
        $('#lessonEnd').val(lesson.endDate.substr(11, 5));
        $('#lessonId').val(lesson.id);
        var studens = [];
        for (let i = 0; i < lesson.students.length; i++) {
            var resource = lesson.students[i];
            $('#lessonStudents').append(`<option value="${i}" selected id=${resource.id}>${resource.name} ${resource.lastName}</option>`);
            studens.push(resource.id);
        }
        SetLessonEndMin();
        LoadLessonModalSelect(lesson.idCourse, lesson.idTeacher, lesson.idRoom, lesson.idCreator, studens);
        $('#lessonModal').modal('show');
    });
}
function OpenCourseDetail(Id) {
    $.getJSON(`http://localhost:49267/api/Courses/GetById/${Id}`)
        .done(function (course) {
        $('#courseForm :input').prop('readonly', true);
        $('#courseReferent').prop('disabled', true);
        $('#saveCourse').hide();
        $('#courseModalTitle').text('Dettagli Percorso');
        LoadResourcesSelect('#courseReferent', true, course.idReferent);
        $('#courseStart').val(new Date(course.startDate).toISOString().substring(0, 10));
        $('#courseEnd').val(new Date(course.endDate).toISOString().substring(0, 10));
        $('#courseDescription').val(course.description);
        $('#courseId').val(course.id);
        $('#courseYear').val(course.referenceYear);
        $('#courseModal').modal('show');
    });
}
function OpenRoomDetail(Id) {
    $.getJSON(`http://localhost:49267/api/Rooms/GetById/${Id}`)
        .done(function (room) {
        $('#roomForm :input').prop('readonly', true);
        $('#saveRoom').hide();
        $('#roomBookable').prop('disabled', true);
        $('#roomModalTitle').text('Dettagli Sala');
        $('#roomNumber').val(room.roomNumber);
        $('#roomPlace').val(room.places);
        $('#roomName').val(room.name);
        $('#roomId').val(room.id);
        $('#roomBookable').prop('checked', room.bookable);
        $('#roomModal').modal('show');
    });
}
//Funzioni per il salvataggio degli elementi
function SaveResource() {
    var resourceForm = $('#resourceForm');
    if (resourceForm[0].checkValidity()) {
        var resource = {
            "name": $('#ResourceName').val(),
            "lastName": $('#ResourceLastName').val()
        };
        $.ajax({
            url: 'http://localhost:49267/api/Resources/Add',
            type: "POST",
            data: JSON.stringify(resource),
            contentType: "application/json;",
            dataType: "json"
        }).done(function (result) {
            OpenResourceDetail(result);
            LoadResources();
        });
    }
    else {
        resourceForm[0].reportValidity();
    }
}
function SaveLesson() {
    var lessonForm = $('#lessonForm');
    if (lessonForm[0].checkValidity()) {
        var lesson = {
            id: parseInt($('#lessonId').val().toString()),
            lessonNumber: parseInt($('#lessonNumber').val().toString()),
            idTeacher: parseInt($('#lessonTeachers').find(':selected').attr('id')),
            idCourse: parseInt($('#lessonCourses').find(':selected').attr('id')),
            idRoom: parseInt($('#lessonRooms').find(':selected').attr('id')),
            idCreator: parseInt($('#lessonCreator').find(':selected').attr('id')),
            startDate: $('#lessonStart').val(),
            endDate: "",
            students: []
        };
        lesson.endDate = lesson.startDate.substring(0, 11) + $('#lessonEnd').val();
        for (let option of $('#lessonStudents option')) {
            var student = {
                id: parseInt(option.id)
            };
            lesson.students.push(student);
        }
        var url, method;
        if (lesson.id === 0) {
            url = 'http://localhost:49267/api/Lessons/Add';
            method = 'POST';
        }
        else {
            url = `http://localhost:49267/api/Lessons/Edit`;
            method = 'PUT';
        }
        $.ajax({
            url: url,
            type: method,
            data: JSON.stringify(lesson),
            contentType: "application/json;",
            dataType: "json"
        }).done(function (result) {
            OpenLessonDetail(result);
            LoadLessons();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            ShowError(jqXHR.responseText);
        });
    }
    else {
        lessonForm[0].reportValidity();
    }
}
function SaveCourse() {
    var courseForm = $('#courseForm');
    if (courseForm[0].checkValidity()) {
        var course = {
            id: parseInt($('#courseId').val().toString()),
            description: $('#courseDescription').val(),
            startDate: $('#courseStart').val(),
            endDate: $('#courseEnd').val(),
            idReferent: parseInt($('#courseReferent').find(':selected').attr('id')),
            referenceYear: parseInt($('#courseYear').val().toString())
        };
        var url, method;
        if (course.id === 0) {
            url = url = 'http://localhost:49267/api/Courses/Add';
            method = 'POST';
        }
        else {
            url = `http://localhost:49267/api/Course/Edit/${course.id}`;
            method = 'PUT';
        }
        $.ajax({
            url: url,
            type: method,
            data: JSON.stringify(course),
            contentType: "application/json;",
            dataType: "json"
        }).done(function (result) {
            OpenCourseDetail(result);
            LoadCourses();
        });
    }
    else {
        courseForm[0].reportValidity();
    }
}
function SaveRoom() {
    var roomForm = $('#roomForm');
    if (roomForm[0].checkValidity()) {
        var room = {
            roomNumber: parseInt($('#roomNumber').val().toString()),
            places: parseInt($('#roomPlace').val().toString()),
            name: $('#roomName').val(),
            bookable: $('#roomBookable').prop('checked')
        };
        $.ajax({
            url: 'http://localhost:49267/api/Rooms/Add',
            type: "POST",
            data: JSON.stringify(room),
            contentType: "application/json;",
            dataType: "json"
        }).done(function (result) {
            OpenRoomDetail(result);
            LoadRooms();
        });
    }
    else {
        roomForm[0].reportValidity();
    }
}
//Funzioni per la gestione delle select all'interno delle modali
function LoadLessonModalSelect(courseId = 0, teacherId = 0, roomId = 0, creatorId = 0, students = undefined) {
    LoadCourseSelect('#lessonCourses', courseId);
    LoadResourcesSelect('#lessonTeachers', true, teacherId);
    LoadResourcesSelect('#globalResources', false, 0, students);
    LoadResourcesSelect('#lessonCreator', true, creatorId);
    LoadRoomsSelect(roomId);
    $('#lessonCourses').on('change', SetLessonDateLimit);
}
function LoadCourseSelect(selectId, courseId = 0) {
    $.getJSON('http://localhost:49267/api/Courses/GetAll')
        .done(function (courses) {
        $(selectId).empty();
        $(selectId).append('<option>--- Seleziona un percorso ---</option>');
        let i = 1;
        for (let course of courses) {
            if (course.id === courseId) {
                $('#lessonCourses').append(`<option value="${i}" selected id=${course.id}>${course.description}</option>`);
            }
            else {
                $('#lessonCourses').append(`<option value="${i}" id=${course.id}>${course.description}</option>`);
            }
            $(`#${course.id}`).on('select', SetLessonDateLimit);
            i++;
        }
    });
}
function LoadResourcesSelect(selectId, useEmptyField, selectedRowId = 0, students = []) {
    $.getJSON('http://localhost:49267/api/Resources/GetAll')
        .done(function (resources) {
        $(selectId).empty();
        if (useEmptyField) {
            $(selectId).append('<option value="">--- Seleziona una Risorsa ---</option>');
        }
        let i = 1;
        for (let res of resources) {
            //Se ci sono degli studenti, significa che sto caricando #globalResources a cui vado ad escludere gli studenti per caricarli direttamente in lessonStudents
            if (students.length > 0) {
                if (students.indexOf(res.id) === -1) {
                    $(selectId).append(`<option value="${i}" selected id=${res.id}>${res.name} ${res.lastName}</option>`);
                }
            }
            else {
                if (res.id === selectedRowId) {
                    $(selectId).append(`<option value="${i}" selected id=${res.id}>${res.name} ${res.lastName}</option>`);
                }
                else {
                    $(selectId).append(`<option value="${i}" id=${res.id}>${res.name} ${res.lastName}</option>`);
                }
            }
        }
    });
}
function LoadRoomsSelect(roomId = 0) {
    $.getJSON('http://localhost:49267/api/Rooms/GetAll')
        .done(function (rooms) {
        $('#lessonRooms').empty();
        $('#lessonRooms').append('<option value="">--- Seleziona una Sala ---</option>');
        let i = 1;
        for (let room of rooms) {
            if (room.bookable) {
                if (room.id === roomId) {
                    $('#lessonRooms').append(`<option value="${i}" selected id=${room.id}>${room.name}</option>`);
                }
                else {
                    $('#lessonRooms').append(`<option value="${i}" id=${room.id}>${room.name}</option>`);
                }
                i++;
            }
        }
    });
}
//Funzioni per la gestione dell'elenco studenti all'interno della modale di inserimento/modifica di una lezione
function AddStudentToList() {
    if ($(`#globalResources`).val() === undefined)
        return;
    var SelectedId = $(`#globalResources`).find(':selected').attr('id');
    var SelectedResource = $(`#globalResources #${SelectedId}`);
    $(`#globalResources #${SelectedId}`).remove();
    $(`#lessonStudents`).append(SelectedResource);
}
function RemoveStudentFromList() {
    if ($(`#lessonStudents`).val() === undefined)
        return;
    var SelectedId = $(`#lessonStudents`).find(':selected').attr('id');
    var SelectedResource = $(`#lessonStudents #${SelectedId}`);
    $(`#lessonStudents #${SelectedId}`).remove();
    $(`#globalResources`).append(SelectedResource);
}
//Funzione che una volta selezionato un percorso all'interno della modale delle lezioni va a modificare le date nei datepicker
function SetLessonDateLimit() {
    var SelectionId = $(this).find(':selected').attr('id');
    $.getJSON(`http://localhost:49267/api/Courses/GetById/${SelectionId}`)
        .done(function (course) {
        $('#lessonStart').attr('min', course.startDate);
        $('#lessonStart').attr('max', course.endDate);
    });
}
//Funzione per settare il minimo orario selezionabile di fine della lezione
function SetLessonEndMin() {
    $('#lessonEnd').attr('min', $('#lessonStart').val().toString().substr(11, 5));
}
//Funzione per settare la minima data selezionabile di un percorso
function SetCourseEndMin() {
    $('#courseEnd').attr('min', $('#courseStart').val());
}
//Funzioni per l'eleminazione degli elementi
function RemoveRoom(Id) {
    $.getJSON(`http://localhost:49267/api/Rooms/CanDelete/${Id}`)
        .done(function (canDelete) {
        if (canDelete) {
            $('#removeElementId').val(Id);
            $('#removeElementType').val('Rooms');
            ShowDeleteModal();
        }
        else {
            ShowError('ATTENZIONE: Impossibile eliminare una sala con lezioni associate');
        }
    });
}
function RemoveLesson(Id) {
    $('#removeElementId').val(Id);
    $('#removeElementType').val('Lessons');
    ShowDeleteModal();
}
function RemoveCourse(Id) {
    $('#removeElementId').val(Id);
    $('#removeElementType').val('Courses');
    ShowDeleteModal();
}
function RemoveElement() {
    var elementID = parseInt($('#removeElementId').val().toString());
    var url = `http://localhost:49267/api/${$('#removeElementType').val()}/Remove/${elementID}`;
    $.ajax({
        url: url,
        type: "DELETE"
    }).done(function (result) {
        ShowSection(sectionIds.indexOf(ActiveSection));
        $('#deleteModal').modal('toggle');
    });
}
//Funzione che reimposta la modale di cancellazione per indicare gli errori
function ShowError(errorText) {
    $('#deleteModalTitle').text('ATTENZIONE');
    $('#removeElement').hide();
    $('#removeDialogText').text(errorText);
    $('#closeRemoveModal').text('Chiudi');
    $('#deleteModal').modal('show');
}
//# sourceMappingURL=index.js.map