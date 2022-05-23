
// Websocket
var connection;
var msgIndex = 0;
var msgQueue = {};

// URL
let req = new URLSearchParams(window.location.search);



/*
 * 
 *   Websocket/Main Functions
 * 
 */
// Send websocket request with callback
function sendRequest(cmd, data, cb) {
    let myReqId = ("id_" + msgIndex);
    msgQueue[myReqId] = cb;
    connection.send(myReqId + "|" + cmd + "|" + data);
    msgIndex++;
}

// Init WebSocket connection
function init() {

    connection = new WebSocket('ws://127.0.0.1:8080');
    connection.onopen = function () {
        sendRequest("connect", 0, function (msg) {
            addMessageToConsole(msg);
            usersInit();
        })
    };

    connection.onmessage = function (msg) {
        // verify msg is ID signed
        if (msg.data.split("|")[0].substring(0,3) == "id_") {
            // get ID
            let res = msg.data.split("|", 1)[0];
            // strip ID
            data = msg.data.substring(msg.data.indexOf("|") + 1);

            if (res in msgQueue) {
                msgQueue[res](data);
            } else {
                console.log("MSG ID not found");
                nonRequestHandler(data);
            }
        // non ID msg
        } else {
            nonRequestHandler(msg);
        }
    };

    document.addEventListener("input", function (event) {
        if (event.target.id == "users-table-select") {
            loadUsersTable(event.target.value);
        }
    });

}

function nonRequestHandler(msg) {
    console.log("Non Request: " + msg.data);
    addMessageToConsole(msg.data);
}



/*
 * 
 *   Scene Functions
 * 
 */
function updateCurrentScene(scene) {document.getElementById("d-status-current-scene").innerHTML = scene;}

function pullCurrentScene() {
    sendRequest("scene", "getScene", function (msg) {
        updateCurrentScene(msg);
    });
}

function changeScene(sceneName) {
    addMessageToConsole("Requesting Scene Change to: " + sceneName);
    sendRequest("scene", "setScene|" + sceneName, function (msg) {
        if (msg != "okay") {
            console.log("Change Scene Error: " + msg);
        } else {
            updateCurrentScene(sceneName);
            addMessageToConsole("Scene Successfully Changed to: " + sceneName);
        }
    });
}



/*
 * 
 *   Users/Table Functions
 * 
 */
function populateUsersTable(data) {
    let tableHeader = '<tr class="user-table-row user-table-header"><th>USERNAME</th><th>NAME</th><th>MODULE 1</th><th>MODULE 2</th><th>MODULE 3</th><th>READING LEVEL</th><th>USER</th><th>DEVICE</th><th>ACTIONS</th></tr>';
    let table = document.getElementById("user-table");
    let rows = data.split("\n");
    let incomplete = '<img src="/home/media/white.png" />&nbsp;';
    let partial = '<img src="/home/media/orange.png" />&nbsp;';
    let complete = '<img src="/home/media/green.png" />';
    let progress = [
        incomplete + incomplete + incomplete,
        partial + incomplete + incomplete,
        complete + incomplete + incomplete,
        complete + partial + incomplete,
        complete + complete + incomplete,
        complete + complete + partial,
        complete + complete + complete,
    ];
    let usertype = ["Student", "Teacher", "Admin"];
    let devicetype = ["KB", "VR"];
    let userdata = { 'users': [] };

    table.innerHTML = tableHeader;
    let rowElement = ""

    for (let i = 0; i < rows.length - 1; i++) {

        let columns = rows[i].split(",");
        let user = {};

        for (let j = 0; j < columns.length; j++) {
            let data = columns[j].split(':');
            user[data[0].trim()] = data[1].trim();
        }

        userdata.users.push(user);

    }

    for (let k = 0; k < userdata.users.length; k++) {

        rowElement += "<tr class='user-table-row'>"
        obj = userdata.users[k];
        rowElement += "<td>" + obj["username"] + "</td><td>" + obj["firstname"] + ", " + obj["lastname"] + "</td>";
        rowElement += '<td><div class="module-status-dot">' + progress[parseInt(obj["module_1"])] + '</div></td>';
        rowElement += '<td><div class="module-status-dot">' + progress[parseInt(obj["module_2"])] + '</div></td>';
        rowElement += '<td><div class="module-status-dot">' + progress[parseInt(obj["module_3"])] + '</div></td>';
        rowElement += "<td>" + obj["readlevel"] + "</td><td>" + usertype[parseInt(obj["permission"])] + "</td><td>" + devicetype[parseInt(obj["vr"])] + "</td>";

        rowElement += "<td><p class='users-table-modify-action' onclick='showUserModify(\"" + obj["username"] + "\")'>Modify User</p></td></tr>"
        table.innerHTML += rowElement;
        rowElement = ""
    }

    table.style.display = "block";
}

function populateUsersData(data) {

    let form = {
        "FirstName":document.getElementById("users-table-show-user-form-FirstName"),
        "LastName": document.getElementById("users-table-show-user-form-LastName"),
        "Username": document.getElementById("users-table-show-user-form-Username"),
        "Permission": document.getElementById("users-table-show-user-form-Permission"),
        "ReadLevel":document.getElementById("users-table-show-user-form-ReadLevel")
    }

    let list = data.split(',');
    for (let i = 0; i < list.length; i++) {
        try {
            form[list[i].split(':')[0]].value = list[i].split(':')[1];
        } catch {
            console.log("Unused data: " + list[i]);
        }

    }

    addMessageToConsole("Fecthed User Data.");

};

function getUserModifyData(username) {
    sendRequest("table", "getUserData|" + username, function (msg) {
        populateUsersData(msg);
    });
}

function showUserModify(username) {
    addMessageToConsole("Fecthing User Data for: " + username);
    getUserModifyData(username);
    showPopup("users-table-show-user");
}

function loadUsersTable(name) {
    if (name == "None") return;
    document.getElementById("user-table-name").innerHTML = name;
    sendRequest("table", "loadTable|" + name, function (msg) {
        populateUsersTable(msg);
    });
}

function usersInit() {
    document.getElementById("users-table-select").innerHTML = "<option>None</option>";
    sendRequest("table", "getTables", function (msg) {
        let tables = msg.split('|');
        loadUsersTable(tables[0]);
        for (let i = 0; i < tables.length; i++) {
            document.getElementById("users-table-select").innerHTML += "<option>" + tables[i] + "</option>";
        }
    });
}

function usersTableCreateList() {
    listName = document.getElementById("users-table-create-list-name").value;
    listFile = document.getElementById("users-table-create-list-file").files;
    let reader = new FileReader();

    reader.onload = function (ev) {

        let data = ev.target.result;
        sendRequest("table", "createUsersList|" + listName + "|" + data, function (msg) {

            hidePopup();
            usersInit();

        });

    };
    reader.readAsText(listFile[0]);
}



/*
 * 
 *   Helper Functions
 * 
 */
function getTimestamp() {
    var t = new Date();
    var time = t.getHours().toString().padStart(2, 0) + ":"
        + t.getMinutes().toString().padStart(2, 0) + ":"
        + t.getSeconds().toString().padStart(2, 0) + ":"
        + t.getMilliseconds().toString().padStart(3, 0);

    return time;
}

function hidePopup() {
    document.getElementById("popups").style.display = "none";
}

function showPopup(name) {
    hidePopups();
    document.getElementById(name).style.display = "inline-block";
    document.getElementById("popups").style.display = "block";
}

function hidePopups() {
    popups = document.getElementsByClassName("popup");
    for (i = 0; i < popups.length; i++) {
        popups[i].style.display = "none";
    }
}

function addMessageToConsole(msg) {
    document.getElementById("status-console-messages").innerHTML += ("<p class=\"console-msg-w\">&nbsp" + getTimestamp() + "&nbsp|&nbsp" + msg + "</p>");
    document.getElementById("status-console").scrollBy(0, 800);
}



/*
 * 
 *   Init
 * 
 */
window.addEventListener("DOMContentLoaded", function () {
    init();
}, false);

