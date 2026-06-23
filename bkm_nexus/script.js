const translations = {
    fr: {
        btnLang: "🌐 English",
        statusOnline: "Actif / Présent",
        statusMeeting: "En Réunion",
        statusOffline: "Absent",
        textProperty: "PROPRIÉTÉ EXCLUSIVE",
        textInstructions: "Ce badge est strictement personnel et requis pour l'accès aux installations.",
        textScanVerify: "Scanner pour vérification",
        panelTitleAdd: "⚙️ Ajouter un Personnel",
        panelTitleEdit: "✏️ Modifier le Personnel",
        labelName: "Nom & Prénom :",
        labelRole: "Poste / Fonction :",
        labelId: "Numéro ID :",
        labelTextColor: "Couleur du texte :",
        labelBgColor: "Couleur de fond (dégradé) :",
        labelBgImage: "Ou Image de fond (Optionnel) :",
        labelPhoto: "Photo de profil :",
        labelStatus: "Statut initial :",
        optOnline: "Présent / Actif",
        optMeeting: "En Réunion",
        optOffline: "Absent",
        btnAdd: "➕ Ajouter à la Planche",
        btnSave: "💾 Sauvegarder le Badge",
        btnClear: "🗑️ Vider la Planche",
        btnPrint: "🖨️ Imprimer la Planche (A4)",
        alertEmpty: "Veuillez remplir au moins le nom et le poste avant d'ajouter."
    },
    en: {
        btnLang: "🌐 Français",
        statusOnline: "Active / Present",
        statusMeeting: "In a Meeting",
        statusOffline: "Absent",
        textProperty: "EXCLUSIVE PROPERTY",
        textInstructions: "This badge is strictly personal and required for facility access.",
        textScanVerify: "Scan for verification",
        panelTitleAdd: "⚙️ Add Personnel",
        panelTitleEdit: "✏️ Edit Personnel",
        labelName: "Full Name:",
        labelRole: "Job Title / Role:",
        labelId: "ID Number:",
        labelTextColor: "Text Color:",
        labelBgColor: "Badge Color:",
        labelBgImage: "Or Background Image (Optional):",
        labelPhoto: "Profile Photo:",
        labelStatus: "Initial Status:",
        optOnline: "Present / Active",
        optMeeting: "In a Meeting",
        optOffline: "Absent",
        btnAdd: "➕ Add to Board",
        btnSave: "💾 Save Badge Changes",
        btnClear: "🗑️ Clear Board",
        btnPrint: "🖨️ Print Board (A4)",
        alertEmpty: "Please fill in at least the name and role before adding."
    }
};

let currentLang = 'fr';
let listPersonnel = []; 
const defaultPhoto = "https://via.placeholder.com/150";
let tempPhotoData = defaultPhoto; 
let tempBgImageData = null; 
let idEnCoursDeModification = null; 

// Éléments du DOM
const btnLang = document.getElementById('btn-lang');
const panelTitle = document.getElementById('panel-title');
const labelName = document.getElementById('label-name');
const labelRole = document.getElementById('label-role');
const labelId = document.getElementById('label-id');
const labelTextColor = document.getElementById('label-text-color');
const labelBgColor = document.getElementById('label-bg-color');
const labelBgImage = document.getElementById('label-bg-image');
const labelPhoto = document.getElementById('label-photo');
const labelStatus = document.getElementById('label-status');
const btnAdd = document.getElementById('btn-add');
const btnClear = document.getElementById('btn-clear');
const btnPrint = document.getElementById('btn-print');
const badgeCountEl = document.getElementById('badge-count');

const inputName = document.getElementById('input-name');
const inputRole = document.getElementById('input-role');
const inputId = document.getElementById('input-id');
const inputTextColor = document.getElementById('input-text-color');
const inputBgColor = document.getElementById('input-bg-color');
const inputBgImage = document.getElementById('input-bg-image');
const inputStatus = document.getElementById('input-status');
const inputPhoto = document.getElementById('input-photo');
const printZone = document.getElementById('print-zone');

// ==========================================
// FONCTIONS DE SAUVEGARDE (LOCALSTORAGE)
// ==========================================
function sauvegarderDansLeNavigateur() {
    localStorage.setItem('sauvegardePlanchePersonnel', JSON.stringify(listPersonnel));
}

function chargerDepuisLeNavigateur() {
    const donneesStockees = localStorage.getItem('sauvegardePlanchePersonnel');
    if (donneesStockees) {
        listPersonnel = JSON.parse(donneesStockees);
    }
}

// Mettre à jour l'interface et la langue
function updateLanguage() {
    const data = translations[currentLang];
    if(btnLang) btnLang.textContent = data.btnLang;
    if(labelName) labelName.textContent = data.labelName;
    if(labelRole) labelRole.textContent = data.labelRole;
    if(labelId) labelId.textContent = data.labelId;
    if(labelTextColor) labelTextColor.textContent = data.labelTextColor;
    if(labelBgColor) labelBgColor.textContent = data.labelBgColor;
    if(labelBgImage) labelBgImage.textContent = data.labelBgImage;
    if(labelPhoto) labelPhoto.textContent = data.labelPhoto;
    if(labelStatus) labelStatus.textContent = data.labelStatus;
    if(btnClear) btnClear.textContent = data.btnClear;
    if(btnPrint) btnPrint.textContent = data.btnPrint;
    
    if(idEnCoursDeModification !== null) {
        if(panelTitle) panelTitle.textContent = data.panelTitleEdit;
        if(btnAdd) btnAdd.textContent = data.btnSave;
    } else {
        if(panelTitle) panelTitle.textContent = data.panelTitleAdd;
        if(btnAdd) btnAdd.textContent = data.btnAdd;
    }
    renderBadges();
}

if(btnLang) {
    btnLang.addEventListener('click', () => {
        currentLang = (currentLang === 'fr') ? 'en' : 'fr';
        updateLanguage();
    });
}

// Convertisseur Fichiers -> Base64 (Photo de profil)
if(inputPhoto) {
    inputPhoto.addEventListener('change', function(e) {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function(event) { tempPhotoData = event.target.result; };
            reader.readAsDataURL(file);
        }
    });
}

// Convertisseur Fichiers -> Base64 (Image de fond arrière-plan)
if(inputBgImage) {
    inputBgImage.addEventListener('change', function(e) {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function(event) { tempBgImageData = event.target.result; };
            reader.readAsDataURL(file);
        }
    });
}

// Traitement Ajout / Modification Formulaire
if(btnAdd) {
    btnAdd.addEventListener('click', () => {
        if(!inputName.value.trim() || !inputRole.value.trim()) {
            alert(translations[currentLang].alertEmpty);
            return;
        }

        const customID = inputId.value.trim() || "#0000";

        if (idEnCoursDeModification !== null) {
            const index = listPersonnel.findIndex(agent => agent.idUnique === idEnCoursDeModification);
            if (index !== -1) {
                listPersonnel[index].nom = inputName.value.trim();
                listPersonnel[index].poste = inputRole.value.trim();
                listPersonnel[index].idCard = customID;
                listPersonnel[index].serial = `SN-2026-${customID.replace('#', '')}`;
                listPersonnel[index].couleurTexte = inputTextColor.value;
                listPersonnel[index].couleurFond = inputBgColor.value;
                listPersonnel[index].statut = inputStatus.value;
                
                if(tempPhotoData !== defaultPhoto) listPersonnel[index].photo = tempPhotoData;
                if(tempBgImageData !== null) listPersonnel[index].bgImage = tempBgImageData;
            }
            idEnCoursDeModification = null;
        } else {
            const nouvelAgent = {
                idUnique: Date.now(),
                nom: inputName.value.trim(),
                poste: inputRole.value.trim(),
                idCard: customID,
                serial: `SN-2026-${customID.replace('#', '')}`,
                couleurTexte: inputTextColor.value,
                couleurFond: inputBgColor.value,
                bgImage: tempBgImageData,
                photo: tempPhotoData,
                statut: inputStatus.value
            };
            listPersonnel.push(nouvelAgent);
        }
        
        // PERSISTANCE : On sauvegarde la liste mise à jour
        sauvegarderDansLeNavigateur();

        // Réinitialisation du formulaire complet
        inputName.value = "";
        inputRole.value = "";
        inputId.value = "";
        inputPhoto.value = "";
        inputBgImage.value = "";
        tempPhotoData = defaultPhoto;
        tempBgImageData = null;

        updateLanguage();
    });
}

window.preparerModification = function(idUnique) {
    const agent = listPersonnel.find(a => a.idUnique === idUnique);
    if (!agent) return;

    idEnCoursDeModification = idUnique;
    inputName.value = agent.nom;
    inputRole.value = agent.poste;
    inputId.value = agent.idCard;
    inputTextColor.value = agent.couleurTexte;
    inputBgColor.value = agent.couleurFond;
    inputStatus.value = agent.statut;
    
    tempPhotoData = agent.photo;
    tempBgImageData = agent.bgImage;

    updateLanguage();
};

window.supprimerBadge = function(idUnique) {
    listPersonnel = listPersonnel.filter(agent => agent.idUnique !== idUnique);
    if(idEnCoursDeModification === idUnique) idEnCoursDeModification = null;
    
    // PERSISTANCE : Mise à jour après suppression
    sauvegarderDansLeNavigateur();
    updateLanguage();
};

if(btnClear) {
    btnClear.addEventListener('click', () => {
        if(confirm("Voulez-vous vraiment vider toute la planche ?")) {
            listPersonnel = [];
            idEnCoursDeModification = null;
            
            // PERSISTANCE : On efface aussi le stockage local
            localStorage.removeItem('sauvegardePlanchePersonnel');
            updateLanguage();
        }
    });
}

function renderBadges() {
    printZone.innerHTML = "";
    if (badgeCountEl) badgeCountEl.textContent = listPersonnel.length;
    const data = translations[currentLang];

    listPersonnel.forEach((agent) => {
        let statutTxt = data.statusOnline;
        if(agent.statut === 'meeting') statutTxt = data.statusMeeting;
        if(agent.statut === 'offline') statutTxt = data.statusOffline;

        const estEnCoursEdite = (agent.idUnique === idEnCoursDeModification) ? 'editing-highlight' : '';

        let styleBackground = `background: linear-gradient(145deg, #ffffff, ${agent.couleurFond}) !important;`;
        if (agent.bgImage) {
            styleBackground = `background-image: url('${agent.bgImage}') !important;`;
        }

        const badgeHTML = `
            <div class="badge-container">
                <div class="badge-actions no-print">
                    <button class="btn-badge-action btn-edit-badge" title="Modifier" onclick="preparerModification(${agent.idUnique})">✏️</button>
                    <button class="btn-badge-action btn-delete-badge" title="Supprimer" onclick="supprimerBadge(${agent.idUnique})">❌</button>
                </div>

                <div class="badge-card ${estEnCoursEdite}" onclick="if(!event.target.closest('.badge-actions')) this.classList.toggle('flipped')">
                    
                    <div class="badge-face badge-front" style="${styleBackground} color: ${agent.couleurTexte} !important;">
                        <div class="badge-header" style="border-bottom-color: ${agent.couleurTexte} !important;">
                            <div class="company-logo" style="background-color: ${agent.couleurTexte} !important;">
                                <span class="logo-icon" style="color: #ffffff !important;">🔒</span>
                            </div>
                            <h2 style="color: ${agent.couleurTexte} !important;">SMART SECURITY</h2>
                        </div>
                        <div class="rfid-chip"></div>
                        <div class="profile-photo" style="border-color: ${agent.couleurTexte} !important;">
                            <img src="${agent.photo}" alt="Profile">
                        </div>
                        <div class="badge-details">
                            <h1 style="color: ${agent.couleurTexte} !important;">${agent.nom}</h1>
                            <p style="color: ${agent.couleurTexte} !important; opacity: 0.9;">${agent.poste}</p>
                            <p class="badge-id" style="color: ${agent.couleurTexte} !important;">ID: ${agent.idCard}</p>
                        </div>
                        <div class="status-indicator-container">
                            <span class="status-dot ${agent.statut}"></span>
                            <span style="color: #1e293b !important;">${statutTxt}</span>
                        </div>
                    </div>

                    <div class="badge-face badge-back">
                        <div class="badge-header-back">
                            <h3>${data.textProperty}</h3>
                        </div>
                        <div class="badge-back-content">
                            <p>${data.textInstructions}</p>
                            <div class="barcode-zone">
                                <div class="barcode-lines">||||| ||| |||| || |||| |||</div>
                                <small>${agent.serial}</small>
                            </div>
                        </div>
                        <div class="badge-footer-back">
                            <small>${data.textScanVerify}</small>
                            <div class="copyright-notice">© Copyright 2026 par Benjamin K. Mazuya</div>
                        </div>
                    </div>

                </div>
            </div>
        `;
        printZone.insertAdjacentHTML('beforeend', badgeHTML);
    });
}

if(btnPrint) {
    btnPrint.addEventListener('click', () => { window.print(); });
}

// INITIALISATION SYNC : Charger les anciennes données avant le premier affichage
chargerDepuisLeNavigateur();
updateLanguage();