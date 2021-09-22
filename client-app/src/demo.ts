// let data : number | string = 42;

// data = '42';


export interface Duck {
    name:string,
    numLegs:number,
    makeSound : (sound : string )=> void,
    optional?:string  // ? işarieti ile undefined oluyor string veya undefined. Objede tanımlanmayabilir.
}

const duck1 : Duck = {
    name:'huey',
    numLegs:2,
    makeSound : (sound : string )=>console.log(sound),
    optional:undefined
}

const duck2 : Duck = {
    name:'dewey',
    numLegs:2,
    makeSound : (sound : string )=>console.log(sound),
    optional:'hop'

}


const s = duck1.name;

export const ducks = [duck1, duck2]