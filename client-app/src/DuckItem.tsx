import { Duck } from './demo';


const DuckItem = function( props : Duck ) {
    
    return(
        <div>
            {props.name}
            <button onClick={p => props.makeSound(`${props.name} + quack `)}>SOUND</button>
        </div>
    );
}

export default DuckItem;