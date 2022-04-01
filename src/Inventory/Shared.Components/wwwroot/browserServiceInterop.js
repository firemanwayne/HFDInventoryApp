// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function getDimensions() {       
        
    window.addEventListener("resize", getInnerDimensions);

    return getInnerDimensions();

    function getInnerDimensions() {

        console.log("height: " + window.innerHeight + " width: " + window.innerWidth);

        return {
            width: parseInt(window.innerWidth),
            height: parseInt(window.innerHeight)
        };
    }   
}
