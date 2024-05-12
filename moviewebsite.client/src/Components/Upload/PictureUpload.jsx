import Popup from 'reactjs-popup';

function PictureUpload(props){
    const [open, setOpen] = useState(false);
    const closeForm = () => {
        setOpen(false);
    };
    return (
        <>
            <Button variant="outline-success" onClick={() => setOpen(o => !o)}>Upload Image</Button>
            <Popup open={openForm} closeOnDocumentClick onClose={closeForm}>

            </Popup>
        </>
    );
}
export default PictureUpload;