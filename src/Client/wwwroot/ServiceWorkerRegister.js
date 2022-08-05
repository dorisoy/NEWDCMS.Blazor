const serviceWorkerFileName = 'service-worker.js';
const swInstalledEvent = 'installed';
//const staticCachePrefix = 'blazor-cache-v';
const blazorAssembly = 'DCMS.Client';
const blazorInstallMethod = 'PWAInstallable';

const notifyNewVersion = () => {
    const bc = new BroadcastChannel('blazor-store-channel');
    bc.postMessage('new-version-found');
}

window.updateAvailable = new Promise(function (resolve, reject) {
    if ('serviceWorker' in navigator) {
        navigator.serviceWorker.register(serviceWorkerFileName)
            .then(function (registration) {
                console.log('Registration successful, scope is:', registration.scope);
                registration.onupdatefound = () => {
                    const installingWorker = registration.installing;
                    installingWorker.onstatechange = () => {
                        switch (installingWorker.state) {
                            case swInstalledEvent:
                                if (navigator.serviceWorker.controller) {
                                    resolve(true);
                                } else {
                                    resolve(false);
                                }
                                break;
                            default:
                        }
                    };
                };
            })
            .catch(error =>
                console.log('Service worker registration failed, error:', error));
    }
});
window['updateAvailable']
    .then(isAvailable => {
        if (isAvailable) {
            notifyNewVersion();
        }
    });

function showAddToHomeScreen() {
    console.log('showAddToHomeScreen');
    DotNet.invokeMethodAsync(blazorAssembly, blazorInstallMethod)
        .then(function () { }, function (er) {
            setTimeout(showAddToHomeScreen, 2500);
        });
}

window.BlazorPWA = {
    installPWA: function () {
        if (window.PWADeferredPrompt) {
            window.PWADeferredPrompt.prompt();
            window.PWADeferredPrompt.userChoice
                .then(function (choiceResult) {
                    window.PWADeferredPrompt = null;
                });
        }
    }
};

window.addEventListener('beforeinstallprompt', function (e) {
    // Prevent Chrome 67 and earlier from automatically showing the prompt
    e.preventDefault();
    // Stash the event so it can be triggered later.
    window.PWADeferredPrompt = e;

    showAddToHomeScreen();
});
