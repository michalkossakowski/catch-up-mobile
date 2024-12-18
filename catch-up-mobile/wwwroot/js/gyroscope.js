let dotNetReference;
let previousOrientation = window.screen.orientation.type;

export function initializeGyroscope(dotNetRef) {
    dotNetReference = dotNetRef;
    
    window.screen.orientation.addEventListener('change', handleOrientationChange);
    
    // Initial check
    handleOrientationChange();
}

export function cleanupGyroscope() {
    window.screen.orientation.removeEventListener('change', handleOrientationChange);
}

function handleOrientationChange() {
    const currentOrientation = window.screen.orientation.type;
    
    if (currentOrientation !== previousOrientation) {
        previousOrientation = currentOrientation;
        const isLandscape = currentOrientation.includes('landscape');
        dotNetReference.invokeMethodAsync('OnOrientationChanged', isLandscape);
    }
} 